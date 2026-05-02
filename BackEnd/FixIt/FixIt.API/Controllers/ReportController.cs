using FixIt.API.Base;
using FixIt.Core.Features.Reports.Command.Models;
using FixIt.Core.Features.Reports.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : AppController
    {


        // Post: Report/submit/ReportedUserId
        [HttpPost("Submit/{ReportedUserid}")]
        [Authorize]
        public async Task<IActionResult> SubmitReport(Guid ReportedUserid, SubmitReportCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            command.ReporterUserId = Id; //Current User
            command.ReportedUserId = ReportedUserid;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);
            return NewResult(result);
        }



        //Get : Reports
        [HttpGet("All Reports")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AllReports()
        {

            var result = await _mediator.Send(new GetAllReportsQuery());
            return NewResult(result);
        }


        // Get : Report/{reportId}
        [HttpGet("GetReport/{ReportId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetReport(int ReportId)
        {

            var result = await _mediator.Send(new GetReportByReportIdQuery(ReportId));
            return NewResult(result);
        }


        // Put : Report/{reportId}/resolve
        [HttpPut("Resolve/{ReportId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ResolveReport(int ReportId, [FromBody] ResolveReportByAdminCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            command.ReportId = ReportId;

            var result = await _mediator.Send(command);
            return NewResult(result);

        }



    }
}
