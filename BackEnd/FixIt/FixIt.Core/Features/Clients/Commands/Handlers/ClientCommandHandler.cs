using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Clients.Commands.Models;
using FixIt.Core.Features.Clients.Queries.DTOs;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Clients.Commands.Handlers
{
    public class ClientCommandHandler : ResponseHandler, IRequestHandler<EditClientCommand, Response<String>>
        ,IRequestHandler<DeleteClientCommand,Response<String>>
        ,IRequestHandler<ChangeClientPasswordCommand,Response<String>>
    {
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        public ClientCommandHandler(IMapper mapper, IClientService clientService)
        {
            _mapper = mapper;
            _clientService = clientService;
        }
        public async Task<Response<string>> Handle(EditClientCommand request, CancellationToken cancellationToken)
        {
            var Client = await _clientService.GetClientById(request.UserId);
            if (Client == null) return NotFound<String>("المستخدم غير موجود");

             _mapper.Map(request,Client);

            var result = await _clientService.EditClinetAsync(Client);

            if (result == "success") return Success("تم التعديل بنجاح");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var Client = await _clientService.GetClientById(request.Id);
            if (Client == null) return NotFound<String>("المستخدم غير موجود");
            var result = await _clientService.DeleteClientAsync(Client);

            if (result == "success") return Deleted<String>("تم الحذف بنجاح");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(ChangeClientPasswordCommand request, CancellationToken cancellationToken)
        {
            var Client = await _clientService.GetClientById(request.UserId);
            if (Client == null) return NotFound<String>("المستخدم غير موجود");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, Client.PasswordHash);
            if (!checkPassword) return BadRequest<string>("كلمة المرور الحاليه غير صحيحه");

            var result = await _clientService.ChangeClinetPasswordAsync(Client, request.NewPassword);
            if (result == "success") return Success("تم التعديل الباسورد بنجاح");
            else return BadRequest<string>();
        }
    }
}
