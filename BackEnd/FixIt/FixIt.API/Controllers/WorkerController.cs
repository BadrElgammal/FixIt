using FixIt.API.Base;
using FixIt.Core.Features.Workers.Commands.Models;
using FixIt.Core.Features.Workers.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : AppController
    {
        //GetAll
        [HttpGet("AllWorkers")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var WorksList = await _mediator.Send(new GetWorkersListQuery());
            return Ok(WorksList);

        }
        //2.worker => workerId

        //GetById
        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> GetById()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var Worker = await _mediator.Send(new GetWorkerByIdQuery(Id));
            return NewResult(Worker);

        }


        //Edite => dtos
        [HttpPut("Edite")]
        [Authorize]
        public async Task<IActionResult> Edite([FromBody] EditeWorkerCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            command.UserId = Id;
            var Worker = await _mediator.Send(command);
            return NewResult(Worker);

        }

        //Delete => dtos
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var Worker = await _mediator.Send(new DeleteWorkerCommand(Id));
            return NewResult(Worker);

        }

        //change password 
        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangeClientPassword([FromBody] ChangeWorkerPasswordCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            command.UserId = Id;
            var result = await _mediator.Send(command);
            return NewResult(result);
        }


        //add protofolio     => اعمال 
        //edite protofolio
        //delete protofolio








    }


}

