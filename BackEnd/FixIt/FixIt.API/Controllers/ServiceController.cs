using FixIt.API.Base;
using FixIt.Core.Features.Service.Commands.Models;
using FixIt.Core.Features.Service.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : AppController
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddService/{WorkerId}")]
        [Authorize]
        public async Task<IActionResult> CreateServieRequest(Guid WorkerId, CreateServiceRequestCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            command.ClientId = Id;
            command.WorkerId = WorkerId;

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpGet("SentsServiceRequests")]
        [Authorize]
        public async Task<IActionResult> GetAllSentsServiceRequests()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new GetSentsServiceRequistQuery(Id));
            return NewResult(result);
        }

        [HttpGet("RecivedsServiceRequests")]
        [Authorize]
        [Authorize(Roles = "worker")]
        public async Task<IActionResult> GetAllRecivedServiceRequests()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new GetRecivedServiceRequestsQuery(Id));
            return NewResult(result);
        }

        [HttpGet("SentsDetails/{serviceId}")]
        [Authorize]
        public async Task<IActionResult> GetSentsServiceRequestDetails(Guid serviceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new GetSentsServiceRequestDetailsQuery(serviceId, Id));
            return NewResult(result);
        }

        [HttpGet("RecivedDetails/{serviceId}")]
        [Authorize]
        [Authorize(Roles = "worker")]
        public async Task<IActionResult> GetRecivedServiceRequestDetails(Guid serviceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new GetRecivedServiceRequestDetailsQuery(serviceId, Id));
            return NewResult(result);
        }



        [HttpPut("{serviceId}/reject")]
        [Authorize]
        public async Task<IActionResult> RejectServiceRequest(Guid serviceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new RejectServiceRequestCommand(serviceId, Id));
            return NewResult(result);
        }

        [HttpPut("recivedJobs/{serviceId}/pending")]
        [Authorize]
        [Authorize(Roles = "worker")]
        public async Task<IActionResult> AddPriceToServiceRequest(Guid serviceId, AddPriceToServiceRequestCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            command.RequestId = serviceId;
            command.WorkerId = Id;

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("recivedJobs/{serviceId}/inprocess")]
        [Authorize]
        public async Task<IActionResult> AcceptPriceServiceRequest(AcceptPriceServiceRequestCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            command.ClientId = Id;

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("recivedJobs/{serviceId}/canceled")]
        [Authorize]
        [Authorize(Roles = "worker")]
        public async Task<IActionResult> CancelServiceRequest(Guid serviceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new CancelServiceRequestCommand(serviceId, Id));
            return NewResult(result);
        }

        [HttpPut("recivedJobs/{serviceId}/submitted")]
        [Authorize]
        [Authorize(Roles = "worker")]
        public async Task<IActionResult> SubmitServiceRequest([FromRoute] Guid serviceId, [FromForm] SubmitServiceRequestCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            command.WorkerId = Id;
            command.ServiceId = serviceId;

            // var result = await _mediator.Send(new SubmitServiceRequestCommand(serviceId,Id));
            var result = await _mediator.Send(command);
            return NewResult(result);
        }


        [HttpPut("recivedJobs/{serviceId}/completed")]
        [Authorize]
        public async Task<IActionResult> AcceptSubmittedServiceRequest(Guid serviceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new AcceptSubmittedServiceRequestCommand(serviceId, Id));
            return NewResult(result);
        }


        [HttpPut("recivedJobs/{serviceId}/Disputed")]
        [Authorize]
        public async Task<IActionResult> DisputedServiceRequest(Guid serviceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new DisputedServiceRequestCommand(serviceId, Id));
            return NewResult(result);
        }
    }
}
