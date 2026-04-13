using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Service.Commands.DTOs;
using FixIt.Core.Features.Service.Queries.DTOs;
using FixIt.Core.Features.Service.Queries.Models;
using FixIt.Core.Wrapper;
using FixIt.Domain.Entities;
using FixIt.Infrastructure.Migrations;
using FixIt.Service.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Queries.Handlers
{
    public class ServiceRequestQueryHandler : ResponseHandler,
                        IRequestHandler<GetSentsServiceRequistQuery, PaginatedResult<GetAllServiceRequistDTO>>,
                        IRequestHandler<GetRecivedServiceRequestsQuery, PaginatedResult<GetAllServiceRequistDTO>>,
                        IRequestHandler<GetAllServiceRequestsQuery, PaginatedResult<GetAllServiceRequistDTO>>,
                        //IRequestHandler<GetSentsServiceRequestDetailsQuery,Response<ServiceRequestDetailsDTO>>,
                        //IRequestHandler<GetRecivedServiceRequestDetailsQuery, Response<ServiceRequestDetailsDTO>>,
                        IRequestHandler<GetServiceRequestDetailsQuery, Response<ServiceRequestDetailsDTO>>

    {
        private readonly IMapper _mapper;
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestQueryHandler(IMapper mapper, IServiceRequestService serviceRequestService)
        {
            _mapper = mapper;
            _serviceRequestService = serviceRequestService;
        }

        public async Task<PaginatedResult<GetAllServiceRequistDTO>> Handle(GetSentsServiceRequistQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ServiceRequest, GetAllServiceRequistDTO>> expression = e => new GetAllServiceRequistDTO(e.RequestId, e.ServiceTitle, e.ServiceDescription, e.TotalPrice, e.State, e.RequestDate, e.Client.FullName, e.Worker.User.FullName, e.ClientId, e.WorkerId, e.RequestedImgUrl, e.SubmitedImgUrl);
            var query = _serviceRequestService.GetAllServiceRequestWithAllDataByClientIdPaginated(request.Id);
            var paginatedList = await query.Select(expression).ToPaginatedListAsync(request.pageNum, request.pageSize);
            return paginatedList;
        }

        public async Task<PaginatedResult<GetAllServiceRequistDTO>> Handle(GetRecivedServiceRequestsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ServiceRequest, GetAllServiceRequistDTO>> expression = e => new GetAllServiceRequistDTO(e.RequestId, e.ServiceTitle, e.ServiceDescription, e.TotalPrice, e.State, e.RequestDate, e.Client.FullName, e.Worker.User.FullName, e.ClientId, e.WorkerId, e.RequestedImgUrl, e.SubmitedImgUrl);
            var query = _serviceRequestService.GetAllServiceRequestWithAllDataByWorkerIdPaginated(request.Id);
            var PaginatedList = await query.Select(expression).ToPaginatedListAsync(request.pageNum, request.pageSize);
            return PaginatedList;
        }
        public async Task<PaginatedResult<GetAllServiceRequistDTO>> Handle(GetAllServiceRequestsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ServiceRequest, GetAllServiceRequistDTO>> expression = e => new GetAllServiceRequistDTO(e.RequestId, e.ServiceTitle, e.ServiceDescription, e.TotalPrice, e.State, e.RequestDate, e.Client.FullName, e.Worker.User.FullName, e.ClientId, e.WorkerId, e.RequestedImgUrl, e.SubmitedImgUrl);
            var query = _serviceRequestService.GetAllServiceRequestForAdminFiltration(request.state);
            var paginatedList = await query.Select(expression).ToPaginatedListAsync(request.pageNum, request.pageSize);
            return paginatedList;
        }

        public async Task<Response<ServiceRequestDetailsDTO>> Handle(GetServiceRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var serviceRequest = await _serviceRequestService.GetServiceRequestWithAllData(request.ServiceId);
            if (request.Role.ToLower() == "client") { if (serviceRequest.ClientId != request.UserId) return BadRequest<ServiceRequestDetailsDTO>("غير مسموح لك عرض بيانات هذا الطلب"); }
            else if (request.Role.ToLower() == "worker")
            {
                var workerId = _serviceRequestService.GetWorkerIdByUserId(request.UserId);
                if (serviceRequest.WorkerId != workerId) return BadRequest<ServiceRequestDetailsDTO>("غير مسموح لك عرض بيانات هذا الطلب");
            }
            else if (request.Role.ToLower() == "admin") { }
            else return BadRequest<ServiceRequestDetailsDTO>("غير مسموح لك عرض بيانات هذا الطلب");

            if(serviceRequest == null) return NotFound<ServiceRequestDetailsDTO>("لا يوجد طلبات بعد");

            var serviceRequestsMapper = _mapper.Map<ServiceRequestDetailsDTO>(serviceRequest);
            return Success(serviceRequestsMapper);

        }
    }
}

