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
        public async Task<IActionResult> CreateServieRequest(Guid WorkerId, [FromForm] CreateServiceRequestCommand command)
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
        public async Task<IActionResult> GetAllSentsServiceRequests([FromQuery] GetSentsServiceRequistQuery query)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            query.Id = Id;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("Worker/RecivedsServiceRequests")]
        [Authorize]
        [Authorize(Roles = "worker")]
        public async Task<IActionResult> GetAllRecivedServiceRequests([FromQuery] GetRecivedServiceRequestsQuery query)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            query.Id = Id;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("/api/Admin/AllServiceRequests")]
        [Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllServiceRequestsToAdmin([FromQuery] GetAllServiceRequestsQuery query)
        {     
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("Details/{serviceId}")]
        [Authorize]
        public async Task<IActionResult> GetServiceRequestDetails(Guid serviceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Role = User.FindFirst(ClaimTypes.Role).Value;
            Guid Id = Guid.Parse(userId);
            
            var result = await _mediator.Send(new GetServiceRequestDetailsQuery(serviceId, Id,Role));
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

        [HttpPut("Worker/recivedJobs/{serviceId}/pending")]
        [Authorize]
        [Authorize(Roles = "worker")]
        public async Task<IActionResult> AddPriceToServiceRequest(Guid serviceId, AddPriceToServiceRequestCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            command.RequestId = serviceId;
            command.UserId = Id;

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("sendsJobs/{serviceId}/inprocess")]
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
        public async Task<IActionResult> CancelServiceRequest(Guid serviceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Rule = User.FindFirst(ClaimTypes.Role).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new CancelServiceRequestCommand(serviceId, Id,Rule));
            return NewResult(result);
        }

        [HttpPut("Worker/recivedJobs/{serviceId}/submitted")]
        [Authorize]
        [Authorize(Roles = "worker")]
        public async Task<IActionResult> SubmitServiceRequest([FromRoute] Guid serviceId, [FromForm] SubmitServiceRequestCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            command.UserId = Id;
            command.ServiceId = serviceId;

            // var result = await _mediator.Send(new SubmitServiceRequestCommand(serviceId,Id));
            var result = await _mediator.Send(command);
            return NewResult(result);
        }


        [HttpPut("sendsJobs/{serviceId}/completed")]
        [Authorize]
        public async Task<IActionResult> AcceptSubmittedServiceRequest(Guid serviceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Rule = User.FindFirst(ClaimTypes.Role).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new AcceptSubmittedServiceRequestCommand(serviceId, Id , Rule));
            return NewResult(result);
        }


        [HttpPut("sendsJobs/{serviceId}/Disputed")]
        [Authorize]
        public async Task<IActionResult> DisputedServiceRequest(Guid serviceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new DisputedServiceRequestCommand(serviceId, Id));
            return NewResult(result);
        }

        [HttpPut("Admin/{serviceId}/resolve")]
        [Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ResolveServiceRequest(Guid serviceId,[FromQuery]string state)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Rule = User.FindFirst(ClaimTypes.Role).Value;
            Guid Id = Guid.Parse(userId);

            if (Rule.ToLower() == "admin" && state.ToLower() == "cancle")
            {
                var result = await _mediator.Send(new CancelServiceRequestCommand(serviceId, Id, Rule));
                return NewResult(result);
            }
            else if (Rule.ToLower() == "admin" && state.ToLower() == "complete")
            {
                var result = await _mediator.Send(new AcceptSubmittedServiceRequestCommand(serviceId, Id, Rule));
                return NewResult(result);
            }
            else
                return BadRequest("خطا ليس لديك صلاحيه او ادخلت امر خطا");
        }
    }
}
