using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Service.Commands.DTOs;
using FixIt.Core.Features.Service.Queries.DTOs;
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
                        IRequestHandler<GetSentsServiceRequistQuery, Response<List<GetAllServiceRequistDTO>>>,
                        IRequestHandler<GetRecivedServiceRequestsQuery, Response<List<GetAllServiceRequistDTO>>>,
                        IRequestHandler<GetSentsServiceRequestDetailsQuery,Response<ServiceRequestDetailsDTO>>,
                        IRequestHandler<GetRecivedServiceRequestDetailsQuery, Response<ServiceRequestDetailsDTO>>

    {
        private readonly IMapper _mapper;
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestQueryHandler(IMapper mapper, IServiceRequestService serviceRequestService)
        {
            _mapper = mapper;
            _serviceRequestService = serviceRequestService;
        }

        public async Task<Response<List<GetAllServiceRequistDTO>>> Handle(GetSentsServiceRequistQuery request, CancellationToken cancellationToken)
        {
            var ServiceRequests = await _serviceRequestService.GetAllServiceRequestWithAllDataByClientId(request.Id);
            if (!ServiceRequests.Any()) return BadRequest<List<GetAllServiceRequistDTO>>("لايوجد طلبات بعد");
            var serviceRequestsMapper = _mapper.Map<List<GetAllServiceRequistDTO>>(ServiceRequests);
            return Success(serviceRequestsMapper);
        }

        public async Task<Response<List<GetAllServiceRequistDTO>>> Handle(GetRecivedServiceRequestsQuery request, CancellationToken cancellationToken)
        {

            var ServiceRequests = await _serviceRequestService.GetAllServiceRequestWithAllDataByWorkerId(request.Id);
            if (!ServiceRequests.Any()) return BadRequest<List<GetAllServiceRequistDTO>>("لايوجد طلبات بعد");
            var serviceRequestsMapper = _mapper.Map<List<GetAllServiceRequistDTO>>(ServiceRequests);
            return Success(serviceRequestsMapper);
        }

        public async Task<Response<ServiceRequestDetailsDTO>> Handle(GetSentsServiceRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var serviceRequest = await _serviceRequestService.GetServiceRequestWithAllData(request.ServiceId);
            if (serviceRequest.ClientId != request.ClientId) return BadRequest<ServiceRequestDetailsDTO>("");
            if (serviceRequest == null) return NotFound<ServiceRequestDetailsDTO>("لا يوجد طلبات بعد");

            var serviceRequestsMapper = _mapper.Map<ServiceRequestDetailsDTO>(serviceRequest);
            return Success(serviceRequestsMapper);
        }

        public async Task<Response<ServiceRequestDetailsDTO>> Handle(GetRecivedServiceRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var workerId = _serviceRequestService.GetWorkerIdByUserId(request.WorkerId);
            var serviceRequest = await _serviceRequestService.GetServiceRequestWithAllData(request.ServiceId);
            if (serviceRequest.WorkerId != workerId) return BadRequest<ServiceRequestDetailsDTO>("");
            if (serviceRequest == null) return NotFound<ServiceRequestDetailsDTO>("لا يوجد طلبات بعد");

            var serviceRequestsMapper = _mapper.Map<ServiceRequestDetailsDTO>(serviceRequest);
            return Success(serviceRequestsMapper);
        }
    }
}

