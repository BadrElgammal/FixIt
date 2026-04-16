using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Clients.Queries.DTOs;
using FixIt.Core.Features.Clients.Queries.Models;
using FixIt.Core.Wrapper;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Clients.Queries.Handlers
{
    public class ClientQueryHandler : ResponseHandler,
        IRequestHandler<GetClientProfileQuery, Response<ClientProfileDTO>>,
        IRequestHandler<GetClientsListQuery, PaginatedResult<User>>
    {
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        public ClientQueryHandler(IMapper mapper, IClientService clientService)
        {
            _mapper = mapper;
            _clientService = clientService;
        }






        public async Task<Response<ClientProfileDTO>> Handle(GetClientProfileQuery request, CancellationToken cancellationToken)
        {
            var Client = await _clientService.GetClientById(request.Id);
            var ClientMapper = _mapper.Map<ClientProfileDTO>(Client);
            return Success(ClientMapper);
        }

        public async Task<PaginatedResult<User>> Handle(GetClientsListQuery request, CancellationToken cancellationToken)
        {
            var clients = _clientService.GetAllClientPaginated();
            var clientsPaginatedList = await clients.ToPaginatedListAsync(request.pageNum, request.pageSize);
            return clientsPaginatedList;
        }
    }
}
