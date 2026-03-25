using FixIt.API.Base;
using FixIt.Core.Features.Clients.Commands.Models;
using FixIt.Core.Features.Clients.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : AppController
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("AllClients")]
        public async Task<IActionResult> GetClients()
        {
            var respose = await _mediator.Send(new GetClientsListQuery());
            return NewResult(respose);
        }

        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> ClientProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            var respose = await _mediator.Send(new GetClientProfileQuery(Id));
            return NewResult(respose);
        }

        [HttpPut("Edit")]
        [Authorize]
        public async Task<IActionResult> EditClientProfile([FromBody] EditClientCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            command.UserId = Id;
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteClientProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            var respose = await _mediator.Send(new DeleteClientCommand(Id));
            return NewResult(respose);
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangeClientPassword([FromBody] ChangeClientPasswordCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            command.UserId = Id;
            var result = await _mediator.Send(command);
            return NewResult(result);
        }


        //ChangeImageURL
        [HttpPut("ChangeImage")]
        [Authorize]
        public async Task<IActionResult> ChangeClientImage([FromForm] ChangeClientImageURL command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);


            command.UserId = Id;
            var result = await _mediator.Send(command);
            return NewResult(result);
        }



    }
}
