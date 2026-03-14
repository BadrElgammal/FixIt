using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Service.Commands.DTOs;
using FixIt.Core.Features.Service.Queries.Models;
using FixIt.Domain.Entities;
using FixIt.Infrastructure.Migrations;
using FixIt.Service.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Queries.Handlers
{
    public class ServiceRequestQueryHandler : ResponseHandler,
                        IRequestHandler<GetSentsServiceRequistQuery, Response<List<ServiceRequestDTO>>>,
                        IRequestHandler<GetRecivedServiceRequestsQuery, Response<List<ServiceRequestDTO>>>,
                        IRequestHandler<GetSentsServiceRequestDetailsQuery,Response<ServiceRequestDTO>>,
                        IRequestHandler<GetRecivedServiceRequestDetailsQuery, Response<ServiceRequestDTO>>

    {
        private readonly IMapper _mapper;
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestQueryHandler(IMapper mapper, IServiceRequestService serviceRequestService)
        {
            _mapper = mapper;
            _serviceRequestService = serviceRequestService;
        }

        public async Task<Response<List<ServiceRequestDTO>>> Handle(GetSentsServiceRequistQuery request, CancellationToken cancellationToken)
        {
            var ServiceRequests =  _serviceRequestService.Find(s => s.ClientId == request.Id).ToList();    
            var serviceRequestsMapper = _mapper.Map<List<ServiceRequestDTO>>(ServiceRequests);
            return Success(serviceRequestsMapper);
        }

        public async Task<Response<List<ServiceRequestDTO>>> Handle(GetRecivedServiceRequestsQuery request, CancellationToken cancellationToken)
        {
            var workerId =  _serviceRequestService.GetWorkerIdByUserId(request.Id);
            var ServiceRequests = _serviceRequestService.Find(s => s.WorkerId == workerId).ToList();
            var serviceRequestsMapper = _mapper.Map<List<ServiceRequestDTO>>(ServiceRequests);
            return Success(serviceRequestsMapper);
        }

        public async Task<Response<ServiceRequestDTO>> Handle(GetSentsServiceRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var servieRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();
            if (servieRequest.ClientId != request.ClientId) return BadRequest<ServiceRequestDTO>("");
            if (servieRequest == null) return NotFound<ServiceRequestDTO>("لا يوجد طلبات بعد");

            var serviceRequestsMapper = _mapper.Map<ServiceRequestDTO>(servieRequest);
            return Success(serviceRequestsMapper);
        }

        public async Task<Response<ServiceRequestDTO>> Handle(GetRecivedServiceRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var workerId = _serviceRequestService.GetWorkerIdByUserId(request.WorkerId);
            var servieRequest = _serviceRequestService.Find(s => s.RequestId == request.ServiceId).FirstOrDefault();
            if (servieRequest.WorkerId != workerId) return BadRequest<ServiceRequestDTO>("");
            if (servieRequest == null) return NotFound<ServiceRequestDTO>("لا يوجد طلبات بعد");

            var serviceRequestsMapper = _mapper.Map<ServiceRequestDTO>(servieRequest);
            return Success(serviceRequestsMapper);
        }
    }
}

