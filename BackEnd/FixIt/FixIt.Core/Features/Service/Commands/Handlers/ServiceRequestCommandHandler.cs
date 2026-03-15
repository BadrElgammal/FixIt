using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Service.Commands.DTOs;
using FixIt.Core.Features.Service.Commands.Models;
using FixIt.Domain.Entities;
using FixIt.Domain.Enum;
using FixIt.Service.Abstracts;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Commands.Handlers
{
    public class ServiceRequestCommandHandler : ResponseHandler, 
                            IRequestHandler<CreateServiceRequestCommand, Response<string>>,
                            IRequestHandler<RejectServiceRequestCommand, Response<string>>,
                            IRequestHandler<AddPriceToServiceRequestCommand, Response<string>>,
                            IRequestHandler<AcceptPriceServiceRequestCommand,Response<string>>,
                            IRequestHandler<CancelServiceRequestCommand , Response<string>>,
                            IRequestHandler<SubmitServiceRequestCommand , Response<string>>,
                            IRequestHandler<AcceptSubmittedServiceRequestCommand , Response<string>>,
                            IRequestHandler<DisputedServiceRequestCommand, Response<string>>

    {
        private readonly IMapper _mapper;
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestCommandHandler(IMapper mapper, IServiceRequestService serviceRequestService)
        {
            _mapper = mapper;
            _serviceRequestService = serviceRequestService;
        }
        public async Task<Response<string>> Handle(CreateServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var serviceRequest = _mapper.Map<ServiceRequest>(request);
            serviceRequest.State = ServiceRequestState.priceprocess;

            var result =await _serviceRequestService.CreateServiceRequest(serviceRequest);
            if (result == "success") return Success("تم  الاضافه الى الخدمات  بنجاح");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(RejectServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var workerId =  _serviceRequestService.GetWorkerIdByUserId(request.UserId);
            var serviceRequest =  _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();
            if((serviceRequest.ClientId == request.UserId && serviceRequest.State == ServiceRequestState.pending) || (serviceRequest.WorkerId == workerId && serviceRequest.State == ServiceRequestState.priceprocess))
            {
                serviceRequest.State = ServiceRequestState.rejected;
                var result = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result == "success") return Success("تم رفض الطلب");
                else return BadRequest<string>();
            }
            else
                return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(AddPriceToServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var workerId = _serviceRequestService.GetWorkerIdByUserId(request.WorkerId);
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.RequestId).FirstOrDefault();
            if(serviceRequest.WorkerId == workerId &&  serviceRequest.State == ServiceRequestState.priceprocess)
            {
                serviceRequest.TotalPrice = request.TotalPrice;
                serviceRequest.State = ServiceRequestState.pending;
                var result = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result == "success") return Success("تم اضافه السعر وبانتظار موافقه العميل");
                else return BadRequest<string>();
            }
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(AcceptPriceServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();
            if(serviceRequest.ClientId == request.ClientId && serviceRequest.State == ServiceRequestState.pending)
            {
               
                var WalletClient = await _serviceRequestService.GetWalletByClientId(request.ClientId);
                var WalletWorker = await _serviceRequestService.GetWalletByWorkerId(serviceRequest.WorkerId);
                var transaction = new Transaction
                {
                    Amount = serviceRequest.TotalPrice,
                    TransactionType = TransactionType.PayDeposit,
                    RefType = "ServiceRequest",
                    FromWalletId = WalletClient.Id,
                    ToWalletId = WalletWorker.Id,
                    Request = serviceRequest
                };
                var result = await _serviceRequestService.CreateTransaction(transaction);
                if (result != "success") return BadRequest<string>();
                WalletClient.Balance -= transaction.Amount;
                serviceRequest.DepositAmount += transaction.Amount;
                serviceRequest.State = ServiceRequestState.inprocess;
                var result2 = await _serviceRequestService.EditWallet(WalletClient);
                if (result2 != "success") return BadRequest<string>();
                var result1 = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result1 != "success") return BadRequest<string>();
                return Success("تم الموافقه وسيتم البدء فى العمل");
            }
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(CancelServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var workerId = _serviceRequestService.GetWorkerIdByUserId(request.WorkerId);
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();
            if (serviceRequest.WorkerId == workerId && serviceRequest.State == ServiceRequestState.inprocess)
            {
                var WalletWorker = await _serviceRequestService.GetWalletByWorkerId(serviceRequest.WorkerId);
                var WalletClient = await _serviceRequestService.GetWalletByClientId(serviceRequest.ClientId);
                var transaction = new Transaction
                {
                    Amount = serviceRequest.TotalPrice,
                    TransactionType = TransactionType.RefundToClient,
                    RefType = "ServiceRequest",
                    FromWalletId = WalletClient.Id,
                    ToWalletId = WalletWorker.Id,
                    Request = serviceRequest

                };
                var result = await _serviceRequestService.CreateTransaction(transaction);
                if (result != "success") return BadRequest<string>();
                WalletClient.Balance += transaction.Amount;
                serviceRequest.DepositAmount -= serviceRequest.TotalPrice;
                serviceRequest.State = ServiceRequestState.canceled;
                var result2 = await _serviceRequestService.EditWallet(WalletClient);
                if (result2 != "success") return BadRequest<string>();
                var result1 = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result1 != "success") return BadRequest<string>();
                return Success("تم ايقاف العمل فى الطلب");
            }
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(SubmitServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var workerId = _serviceRequestService.GetWorkerIdByUserId(request.WorkerId);
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();
            if (serviceRequest.WorkerId == workerId && serviceRequest.State == ServiceRequestState.inprocess)
            {
                serviceRequest.State = ServiceRequestState.submitted;
                var result1 = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result1 != "success") return BadRequest<string>();
                return Success("تم تسليم العمل وبانتظار التاكيد");

            }
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(AcceptSubmittedServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();
            if (serviceRequest.ClientId == request.ClientId && serviceRequest.State == ServiceRequestState.submitted)
            {
                var WalletWorker = await _serviceRequestService.GetWalletByWorkerId(serviceRequest.WorkerId);
                var WalletClient = await _serviceRequestService.GetWalletByClientId(serviceRequest.ClientId);
                var transaction = new Transaction
                {
                    Amount = serviceRequest.TotalPrice * (decimal)0.85,
                    ServiceCommetion = serviceRequest.TotalPrice * (decimal)0.15,
                    TransactionType = TransactionType.ReleaseToWorker,
                    RefType = "ServiceRequest",
                    FromWalletId = WalletClient.Id,
                    ToWalletId = WalletWorker.Id,
                    Request = serviceRequest
                }; 
                var result = await _serviceRequestService.CreateTransaction(transaction);
                if (result != "success") return BadRequest<string>();
                WalletWorker.Balance += transaction.Amount;
                serviceRequest.DepositAmount -= serviceRequest.TotalPrice;
                serviceRequest.State = ServiceRequestState.completed;
                var result2 = await _serviceRequestService.EditWallet(WalletWorker);
                if (result2 != "success") return BadRequest<string>();
                var result1 = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result1 != "success") return BadRequest<string>();
                return Success(" تم تاكيد استلام العمل  ");
            }
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DisputedServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();
            if (serviceRequest.ClientId == request.ClientId && serviceRequest.State == ServiceRequestState.submitted)
            {
                serviceRequest.State = ServiceRequestState.disputed;
                var result1 = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result1 != "success") return BadRequest<string>();
                return Success("تم تحويل الطلب الى الدعم");

            }
            return BadRequest<string>();
        }
    }
}
