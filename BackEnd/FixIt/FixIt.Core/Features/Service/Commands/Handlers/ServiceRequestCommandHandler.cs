using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Notifications.Commands.Models;
using FixIt.Core.Features.Service.Commands.Models;
using FixIt.Domain.Entities;
using FixIt.Domain.Enum;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Service.Commands.Handlers
{
    public class ServiceRequestCommandHandler : ResponseHandler,
                            IRequestHandler<CreateServiceRequestCommand, Response<string>>,
                            IRequestHandler<RejectServiceRequestCommand, Response<string>>,
                            IRequestHandler<AddPriceToServiceRequestCommand, Response<string>>,
                            IRequestHandler<AcceptPriceServiceRequestCommand, Response<string>>,
                            IRequestHandler<CancelServiceRequestCommand, Response<string>>,
                            IRequestHandler<SubmitServiceRequestCommand, Response<string>>,
                            IRequestHandler<AcceptSubmittedServiceRequestCommand, Response<string>>,
                            IRequestHandler<DisputedServiceRequestCommand, Response<string>>

    {
        private readonly IMapper _mapper;
        private readonly IServiceRequestService _serviceRequestService;
        private readonly IMediator _mediator;

        public ServiceRequestCommandHandler(IMapper mapper, IServiceRequestService serviceRequestService ,IMediator mediator)
        {
            _mapper = mapper;
            _serviceRequestService = serviceRequestService;
            _mediator = mediator;
        }
        public async Task<Response<string>> Handle(CreateServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var serviceRequest = _mapper.Map<ServiceRequest>(request);
            serviceRequest.State = ServiceRequestState.priceprocess;
            //create ServiceRequest
            var result = await _serviceRequestService.CreateServiceRequest(serviceRequest, request.RequestedImgUrl);
            if (result != "success") return BadRequest<string>();

            var UserId = await _serviceRequestService.GetUserIdByWorkerId(request.WorkerId);
            //Add Notification
            await _mediator.Send(new AddNotificationCommand(

                UserId: UserId, 
                title: "طلب خدمه جديده",
                message: $"لديك طلب خدمه جديد",
                notificationType: NotificationType.Service,
                relatedEntityId: serviceRequest.RequestId.ToString()));


            return Success("تم  الاضافه الى الخدمات  بنجاح");
        }

        public async Task<Response<string>> Handle(RejectServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var workerId = _serviceRequestService.GetWorkerIdByUserId(request.UserId);
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();

            //check the user is client or worker in service and check the state
            if ((serviceRequest.ClientId == request.UserId && serviceRequest.State == ServiceRequestState.pending) || (serviceRequest.WorkerId == workerId && serviceRequest.State == ServiceRequestState.priceprocess))
            {
                //Edite ServiceRequest State
                serviceRequest.State = ServiceRequestState.rejected;
                var result = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result != "success") return BadRequest<string>();

                //Add Notification
                await _mediator.Send(new AddNotificationCommand(
                    // لو العميل هو الى داخل يبقى هجيب ادى اليوزر بتاع العامل من خلال الخدمه
                    UserId: serviceRequest.ClientId == request.UserId ? await _serviceRequestService.GetUserIdByWorkerId(serviceRequest.WorkerId) : serviceRequest.ClientId,
                    title: "رفض الخدمه",
                    message: $"تم رفض الخدمه : {serviceRequest.ServiceTitle}",
                    notificationType: NotificationType.Service,
                    relatedEntityId: serviceRequest.RequestId.ToString()));

                return Success("تم رفض الطلب");
            }
            else
                return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(AddPriceToServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var workerId = _serviceRequestService.GetWorkerIdByUserId(request.UserId);
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.RequestId).FirstOrDefault();
            
            //check the user is worker in service and check the state
            if (serviceRequest.WorkerId == workerId && serviceRequest.State == ServiceRequestState.priceprocess)
            {
                //Edite ServiceRequest State and add price by worker
                serviceRequest.TotalPrice = request.TotalPrice;
                serviceRequest.State = ServiceRequestState.pending;
                var result = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result != "success") return BadRequest<string>();

                //Add Notification
                await _mediator.Send(new AddNotificationCommand(

                    UserId: serviceRequest.ClientId,
                    title: "مراجعه السعر الخدمه",
                    message: $"تم قبول الخدمه يرجى مراجعه السعر والموافقه او الرفض",
                    notificationType: NotificationType.Service,
                    relatedEntityId: serviceRequest.RequestId.ToString()));


                return Success("تم اضافه السعر وبانتظار موافقه العميل");
            }
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(AcceptPriceServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();
            
            //check the user is client in service and check the state
            if (serviceRequest.ClientId == request.ClientId && serviceRequest.State == ServiceRequestState.pending)
            {
                //get wallet of client and check the ballance
                var WalletClient = await _serviceRequestService.GetWalletByClientId(request.ClientId);
                if (WalletClient.Balance < serviceRequest.TotalPrice)
                    return BadRequest<string>("لا يوجد مال كافى لديك فى المحفظه");

                var WalletWorker = await _serviceRequestService.GetWalletByWorkerId(serviceRequest.WorkerId);
                //Add Transaction
                var transaction = new Transaction
                {
                    Amount = serviceRequest.TotalPrice,
                    TransactionType = TransactionType.PayDeposit,
                    RefType = "ServiceRequest",
                    FromWalletId = WalletClient.Id,
                    ToWalletId = WalletWorker.Id,
                    Request = serviceRequest
                };
                //Create Transaction
                var result = await _serviceRequestService.CreateTransaction(transaction);
                if (result != "success") return BadRequest<string>();

                //Deducting funds from the Client's Wallet and deposit it in service and Edit Wallet
                WalletClient.Balance -= transaction.Amount;
                serviceRequest.DepositAmount += transaction.Amount;
                var result2 = await _serviceRequestService.EditWallet(WalletClient);
                if (result2 != "success") return BadRequest<string>();

                //Change state and ServiceAddress and Edit Service 
                serviceRequest.State = ServiceRequestState.inprocess;
                serviceRequest.serviceAddress = request.serviceAddress;
                var result1 = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result1 != "success") return BadRequest<string>();

                var UserId = await _serviceRequestService.GetUserIdByWorkerId(serviceRequest.WorkerId);
                #region Notification
                //Add Notification
                await _mediator.Send(new AddNotificationCommand(

                    UserId: UserId,
                    title: $"تحديث فى حاله الخدمه : {serviceRequest.ServiceTitle}",
                    message: $"تم الموافقه على السعر يرجى البدء فى العمل",
                    notificationType: NotificationType.Service,
                    relatedEntityId: serviceRequest.RequestId.ToString()));

                //Add Notification
                await _mediator.Send(new AddNotificationCommand(

                    UserId: serviceRequest.ClientId,
                    title: $"تحديث المحفظه",
                    message: $"تم خصم المبلغ : {transaction.Amount} \nمقابل الخدمه : {serviceRequest.ServiceTitle} \nوسيتم البدء فى العمل فورا",
                    notificationType: NotificationType.Wallet,
                    relatedEntityId: serviceRequest.RequestId.ToString()));
                #endregion


                return Success("تم الموافقه وسيتم البدء فى العمل");
            }
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(CancelServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var workerId = _serviceRequestService.GetWorkerIdByUserId(request.UserId);
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();
          
            //check the user is worker in service and check the state
            if (serviceRequest.WorkerId == workerId && serviceRequest.State == ServiceRequestState.inprocess)
            {
                var WalletWorker = await _serviceRequestService.GetWalletByWorkerId(serviceRequest.WorkerId);
                var WalletClient = await _serviceRequestService.GetWalletByClientId(serviceRequest.ClientId);

                //Add Transaction
                var transaction = new Transaction
                {
                    Amount = serviceRequest.TotalPrice,
                    TransactionType = TransactionType.RefundToClient,
                    RefType = "ServiceRequest",
                    FromWalletId = WalletClient.Id,
                    ToWalletId = WalletWorker.Id,
                    Request = serviceRequest

                };
                //Create Transaction
                var result = await _serviceRequestService.CreateTransaction(transaction);
                if (result != "success") return BadRequest<string>();

                //Deducting funds from the service and deposit it in Client's Wallet and Edit Wallet
                WalletClient.Balance += transaction.Amount;
                serviceRequest.DepositAmount -= serviceRequest.TotalPrice;
                var result2 = await _serviceRequestService.EditWallet(WalletClient);
                if (result2 != "success") return BadRequest<string>();

                //Change state and Edit Service 
                serviceRequest.State = ServiceRequestState.canceled;
                var result1 = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result1 != "success") return BadRequest<string>();


                #region Notification
                //Add Notification
                await _mediator.Send(new AddNotificationCommand(

                    UserId: serviceRequest.ClientId,
                    title: $"تحديث فى حاله الخدمه : {serviceRequest.ServiceTitle}",
                    message: $"تم ايقاف العمل فى الطلب",
                    notificationType: NotificationType.Service,
                    relatedEntityId: serviceRequest.RequestId.ToString()));

                //Add Notification
                await _mediator.Send(new AddNotificationCommand(

                    UserId: serviceRequest.ClientId,
                    title: $"تحديث المحفظه",
                    message: $"تم ايداع المبلغ : {transaction.Amount} \nمقابل الغاء الخدمه : {serviceRequest.ServiceTitle}",
                    notificationType: NotificationType.Wallet,
                    relatedEntityId: serviceRequest.RequestId.ToString()));
                #endregion
                return Success("تم ايقاف العمل فى الطلب");
            }
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(SubmitServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var workerId = _serviceRequestService.GetWorkerIdByUserId(request.UserId);
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();

            //check the user is worker in service and check the state
            if (serviceRequest.WorkerId == workerId && serviceRequest.State == ServiceRequestState.inprocess)
            {
                //Change state and Edit Service 
                serviceRequest.State = ServiceRequestState.submitted;
                var result1 = await _serviceRequestService.EditServiceRequestAsync(serviceRequest, request.SubmitedImgUrl);
                if (result1 != "success") return BadRequest<string>();


                #region Notification
                //Add Notification
                await _mediator.Send(new AddNotificationCommand(

                    UserId: serviceRequest.ClientId,
                    title: $"تحديث فى حاله الخدمه : {serviceRequest.ServiceTitle}",
                    message: $"تم تسليم العمل يرجى التاكيد على التنفيذ",
                    notificationType: NotificationType.Service,
                    relatedEntityId: serviceRequest.RequestId.ToString()));

                #endregion
                return Success("تم تسليم العمل وبانتظار التاكيد");

            }
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(AcceptSubmittedServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();

            //check the user is worker in service and check the state
            if (serviceRequest.ClientId == request.ClientId && serviceRequest.State == ServiceRequestState.submitted)
            {
                var WalletWorker = await _serviceRequestService.GetWalletByWorkerId(serviceRequest.WorkerId);
                var WalletClient = await _serviceRequestService.GetWalletByClientId(serviceRequest.ClientId);
                if (serviceRequest.TotalPrice != serviceRequest.DepositAmount)
                    return BadRequest<string>();

                //Add Transaction
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
                //Create Transaction
                var result = await _serviceRequestService.CreateTransaction(transaction);
                if (result != "success") return BadRequest<string>();

                //Deducting funds from the service and deposit it in Worker's Wallet and Edit Wallet
                WalletWorker.Balance += transaction.Amount;
                serviceRequest.DepositAmount -= serviceRequest.TotalPrice;
                var result2 = await _serviceRequestService.EditWallet(WalletWorker);
                if (result2 != "success") return BadRequest<string>();

                //Change state and Edit Service 
                serviceRequest.State = ServiceRequestState.completed;
                var result1 = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result1 != "success") return BadRequest<string>();

                var UserId = await _serviceRequestService.GetUserIdByWorkerId(serviceRequest.WorkerId);
                #region Notification
                //Add Notification
                await _mediator.Send(new AddNotificationCommand(

                    UserId: UserId,
                    title: $"تحديث فى حاله الخدمه : {serviceRequest.ServiceTitle}",
                    message: $"تم تاكيد تنفيذ وانتهاء الطلب",
                    notificationType: NotificationType.Service,
                    relatedEntityId: serviceRequest.RequestId.ToString()));

                //Add Notification
                await _mediator.Send(new AddNotificationCommand(

                    UserId: UserId,
                    title: $"تحديث المحفظه",
                    message: $"تم ايداع المبلغ : {transaction.Amount} \nمقابل اتمام الخدمه : {serviceRequest.ServiceTitle}",
                    notificationType: NotificationType.Wallet,
                    relatedEntityId: serviceRequest.RequestId.ToString()));
                #endregion
                return Success(" تم تاكيد استلام العمل  ");
            }
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DisputedServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();

            //check the user is Client in service and check the state
            if (serviceRequest.ClientId == request.ClientId && serviceRequest.State == ServiceRequestState.submitted)
            {
                //Change state and Edit Service 
                serviceRequest.State = ServiceRequestState.disputed;
                var result1 = await _serviceRequestService.EditServiceRequestAsync(serviceRequest);
                if (result1 != "success") return BadRequest<string>();

                var UserId = await _serviceRequestService.GetUserIdByWorkerId(serviceRequest.WorkerId);
                #region Notification
                //Add Notification
                await _mediator.Send(new AddNotificationCommand(

                    UserId: serviceRequest.ClientId,
                    title: $"تحديث فى حاله الخدمه : {serviceRequest.ServiceTitle}",
                    message: $"تم الابلاغ عن عدم اتمام الخدمه وتم تحويلها الى الدعم وسيتم التواصل معكم وشكرا",
                    notificationType: NotificationType.Service,
                    relatedEntityId: serviceRequest.RequestId.ToString()));

                await _mediator.Send(new AddNotificationCommand(

                    UserId: UserId,
                    title: $"تحديث فى حاله الخدمه : {serviceRequest.ServiceTitle}",
                    message: $"تم الابلاغ عن عدم اتمام الخدمه وتم تحويلها الى الدعم وسيتم التواصل معكم وشكرا",
                    notificationType: NotificationType.Service,
                    relatedEntityId: serviceRequest.RequestId.ToString()));
                #endregion
                return Success("تم تحويل الطلب الى الدعم");

            }
            return BadRequest<string>();
        }
    }
}
