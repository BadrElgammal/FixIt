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
        //All Reviews
        [HttpGet("AllReviews")]
        [Authorize]
        public async Task<IActionResult> AllReviews()
        {
            var respose = await _mediator.Send(new GetReviewsListQuery());
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
