using FixIt.API.Base;
using FixIt.Core.Features.Reviews.Command.Models;
using FixIt.Core.Features.Reviews.Query.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : AppController
    {
        //Admin
        [HttpGet("AllReviewsAdmin")]
        [Authorize]
        public async Task<IActionResult> AllReviewsForAdmin()
        {
            var respose = await _mediator.Send(new GetReviewsListQuery());
            return NewResult(respose);
        }


        //All Reviews {ByWorkerId}
        [HttpGet("AllReviewsByWorkerId/{WorkerId}")]
        [Authorize]
        public async Task<IActionResult> AllReviewsByWorker(Guid WorkerId)
        {

            var respose = await _mediator.Send(new GetReviewsListByWorkerIdQuery(WorkerId));
            return NewResult(respose);
        }



        //All Reviews for me [UserId]
        [HttpGet("AllReviews")]
        [Authorize]
        public async Task<IActionResult> AllReviews()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);


            var respose = await _mediator.Send(new GetMyAllReviewsListQuery(Id));
            return NewResult(respose);
        }



        //Add Review /{workerId}
        [HttpPost("AddReview/{ServiceId}")]
        [Authorize]
        public async Task<IActionResult> AddReview(Guid ServiceId, AddReviewCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            command.ReviewerId = Id;
            command.RequestId = ServiceId;
            //command.ReviewedWorkerId = WorkerId;

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        //Delete Review/{workerId}
        [HttpDelete("DeleteReview/{ReviewId}")]
        [Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteReview(int ReviewId)
        {

            var result = await _mediator.Send(new DeleteReviewCommand(ReviewId));
            return NewResult(result);
        }


    }
}
