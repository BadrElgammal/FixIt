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
        [HttpGet("/api/Admin/AllReviewsAdmin")]
        [Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AllReviewsForAdmin([FromQuery] int PageNum, [FromQuery] int PageSize)
        {
            GetReviewsListQuery query = new GetReviewsListQuery
            {
                pageNum = PageNum,
                pageSize = PageSize
            };
            var respose = await _mediator.Send(query);
            return Ok(respose);
        }


        //All Reviews {ByWorkerId}
        [HttpGet("AllReviewsByWorkerId/{WorkerId}")]
        [Authorize]
        public async Task<IActionResult> AllReviewsByWorker([FromRoute]Guid WorkerId, [FromQuery] int PageNum, [FromQuery] int PageSize)
        {
            GetReviewsListByWorkerIdQuery query = new GetReviewsListByWorkerIdQuery
            {
                pageNum = PageNum,
                pageSize = PageSize,
                workerId = WorkerId
            };
            var respose = await _mediator.Send(query);
            return Ok(respose);
        }



        //All Reviews for me [UserId]
        [HttpGet("AllReviews")]
        [Authorize]
        public async Task<IActionResult> AllReviews([FromQuery]  int PageNum , [FromQuery] int PageSize)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            GetMyAllReviewsListQuery query = new GetMyAllReviewsListQuery
            {
                pageNum = PageNum,
                pageSize = PageSize,
                userId = Id
            };
            var respose = await _mediator.Send(query);
            return Ok(respose);
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
