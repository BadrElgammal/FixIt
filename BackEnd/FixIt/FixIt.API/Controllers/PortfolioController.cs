using FixIt.API.Base;
using FixIt.Core.Features.Portfolios.Command.Models;
using FixIt.Core.Features.Portfolios.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : AppController
    {


        //add Portfolio    
        [HttpPost("AddPortfolio")]
        [Authorize]
        public async Task<IActionResult> Add([FromForm] AddPortfolioCommand command)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            command.WorkerProfileId = Id;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respose = await _mediator.Send(command);
            return NewResult(respose);
        }

        //edite Portfolio{PortfolioId}
        [HttpPut("EditePortfolio/{portfolioId}")]
        [Authorize]
        public async Task<IActionResult> Edite([FromRoute] int portfolioId, [FromForm] EditePortfolioCommand command)
        {


            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            command.PortfolioId = portfolioId;
            command.WorkerProfileId = Id;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respose = await _mediator.Send(command);
            return NewResult(respose);
        }

        //delete Portfolio{PortfolioId}
        [HttpDelete("DeletePortfolio/{portfolioId}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int portfolioId)
        {

            var respose = await _mediator.Send(new DeletePortfolioCommand(portfolioId));
            return NewResult(respose);
        }

        //// AllPortfoliosForAdmin
        //[HttpGet("AllPortfoliosForAdmin")]
        //[Authorize]
        //public async Task<IActionResult> GetAllForAdmin()
        //{

        //    var PortoliosList = await _mediator.Send(new GetAllPortfoliosForAdminQuery());
        //    return NewResult(PortoliosList);
        //}


        //All Portfolios in [workerId]
        [HttpGet("AllPortfoliosByWorkerId/{workerId}")]
        [Authorize]
        public async Task<IActionResult> GetAllByWorkerId([FromBody]Guid WorkerId , [FromQuery] int PageNum, [FromQuery] int PageSize)
        {
            GetPortoliosListByWorkerIdQuery query = new GetPortoliosListByWorkerIdQuery
            {
                pageNum = PageNum,
                pageSize = PageSize,
                WorkerId = WorkerId
            };
            var PortoliosList = await _mediator.Send(query);
            return Ok(PortoliosList);
        }

        //All Portfolios in [UserId] for me [tocken]
        [HttpGet("AllPortfoliosByUserId")]
        [Authorize]
        public async Task<IActionResult> GetAllByUserId([FromQuery] int PageNum, [FromQuery] int PageSize)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            GetAllPortfoliosByUserIdQuery query = new GetAllPortfoliosByUserIdQuery
            {
                pageNum = PageNum,
                pageSize = PageSize,
                UserId = Id
            };

            var PortoliosList = await _mediator.Send(query);
            return Ok(PortoliosList);
        }




    }
}
