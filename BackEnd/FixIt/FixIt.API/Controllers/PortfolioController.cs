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

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.PortfolioId = portfolioId;

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

        //All Portfolios in [workerId]
        [HttpGet("AllPortfoliosByWorkerId/{workerId}")]
        [Authorize]
        public async Task<IActionResult> GetAllByWorkerId(Guid WorkerId)
        {

            var PortoliosList = await _mediator.Send(new GetPortoliosListByWorkerIdQuery(WorkerId));
            return NewResult(PortoliosList);
        }

        //All Portfolios in [UserId]
        [HttpGet("AllPortfoliosByUserId")]
        [Authorize]
        public async Task<IActionResult> GetAllByUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var PortoliosList = await _mediator.Send(new GetAllPortfoliosByUserIdQuery(Id));
            return NewResult(PortoliosList);
        }


    }
}
