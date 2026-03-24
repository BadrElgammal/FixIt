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
        public async Task<IActionResult> Add([FromBody] AddPortfolioCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respose = await _mediator.Send(command);
            return Ok(respose);
        }

        //edite Portfolio
        [HttpPut("EditePortfolio/{id}")]
        [Authorize]
        public async Task<IActionResult> Edite([FromRoute] int id)
        {
            var respose = await _mediator.Send(new EditePortfolioCommand(id));
            return Ok(respose);
        }

        //delete Portfolio
        [HttpDelete("DeletePortfolio/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var respose = await _mediator.Send(new DeletePortfolioCommand(id));
            return Ok(respose);
        }

        //All Portfolios in [workerId]
        [HttpGet("AllPortfoliosByWorkerId/{workerId}")]
        [Authorize]
        public async Task<IActionResult> GetAllByWorkerId(Guid WorkerId)
        {

            var PortoliosList = await _mediator.Send(new GetPortoliosListByWorkerIdQuery(WorkerId));
            return Ok(PortoliosList);
        }

        //All Portfolios in [UserId]
        [HttpGet("AllPortfoliosByUserId")]
        [Authorize]
        public async Task<IActionResult> GetAllByUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var PortoliosList = await _mediator.Send(new GetAllPortfoliosByUserIdQuery(Id));
            return Ok(PortoliosList);
        }


        //AddImage
        [HttpPut("AddPortfolioImage/{id}")]
        [Authorize]
        public async Task<IActionResult> AddImage([FromRoute] int id, [FromBody] AddPortfolioImgURlCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            command.PortfolioId = id;

            var respose = await _mediator.Send(command);
            return Ok(respose);
        }


    }
}
