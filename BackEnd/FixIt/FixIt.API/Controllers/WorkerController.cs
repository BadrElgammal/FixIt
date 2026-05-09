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
        public async Task<IActionResult> GetAll([FromQuery] GetWorkersPaginatedListQuery query)
        {
            var WorksList = await _mediator.Send(query);
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

            var Worker = await _mediator.Send(new GetWorkerByUserIdQuery(Id));
            return NewResult(Worker);

        }


        //GetByWorkerId
        [HttpGet("WorkerProfile/{WorkerId}")]
        [Authorize]
        public async Task<IActionResult> GetByWorkerId(Guid WorkerId)
        {

            var Worker = await _mediator.Send(new GetWorkerProfileByWorkerIdQuery(WorkerId));
            return NewResult(Worker);

        }



        //Edite => dtos
        [HttpPut("Edite")]
        [Authorize]
        public async Task<IActionResult> Edite([FromBody] EditeWorkerCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
        public async Task<IActionResult> ChangeWorkerPassword([FromBody] ChangeWorkerPasswordCommand command)
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
        public async Task<IActionResult> ChangeWorkerImage([FromForm] ChangeWorkerImgURL command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);


            command.userId = Id;
            var result = await _mediator.Send(command);
            return NewResult(result);
        }


        // Post : Client/AddWorker => By Admin
        [HttpPost("AddWorker")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddWorker(AddWorkerByAdminCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        //1.1.get Last Services for this worker
        [HttpGet("GetLast/{SelectedNumber?}/Services")]
        [Authorize]
        public async Task<IActionResult> GetLastServicesForWorker([FromRoute] int? SelectedNumber = null)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);


            var command = new GetLastServicesRequestByUserIdQuery(Id)
            {
                userId = Id,
                SelectedNumber = SelectedNumber
            };

            var result = await _mediator.Send(command);
            return NewResult(result);

        }

        //2.get recent 3 massage
        [HttpGet("GetLast/{SelectedNumber?}/Messasges")]
        [Authorize]
        public async Task<IActionResult> GetLastMessasgesForWorker([FromRoute] int? SelectedNumber = null)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);


            var command = new GetLastMassagesQuery(Id)
            {
                UserId = Id,
                SelectedNumber = SelectedNumber
            };

            var result = await _mediator.Send(command);
            return NewResult(result);

        }

        //3.get recent 3 reviews
        [HttpGet("GetLast/{SelectedNumber?}/Reviews")]
        [Authorize]
        public async Task<IActionResult> GetLastReviewsForWorker([FromRoute] int? SelectedNumber = null)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);


            var command = new GetLastReviewsQuery(Id)
            {
                userId = Id,
                SelectedNumber = SelectedNumber
            };

            var result = await _mediator.Send(command);
            return NewResult(result);

        }

        //4.get total incom for month
        //5.TOoooooooooooooooootal
        //.total services
        //.total reviews
        //.total prtofolioes
        //.total services
        [HttpGet("GetLast/TotalNumbersDetailsForWorker")]
        [Authorize]
        public async Task<IActionResult> GetDetailsForWorker()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new GetTotalDetailsForWorker(Id));
            return NewResult(result);

        }

    }

}

