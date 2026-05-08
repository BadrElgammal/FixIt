using FixIt.API.Base;
using FixIt.Core.Features.Admin.Command.Models;
using FixIt.Core.Features.Admin.Query.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : AppController
    {

        //Get : Admin/profile
        [HttpGet("Profile")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var admin = await _mediator.Send(new GetAdminProfileQuery(Id));
            return NewResult(admin);

        }

        //Put : Admin/Edit
        [HttpPut("Edit")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditAdminProfile([FromBody] EditeAdminCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            command.UserId = Id;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        //Delete : Admin/Delete
        [HttpDelete("Delete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAdminProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var respose = await _mediator.Send(new DeleteAdminCommand(Id));
            return NewResult(respose);
        }


        //Put : Admin/changepassword
        [HttpPut("ChangePassword")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeAdminPassword([FromBody] ChangeAdminPasswordCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            command.UserId = Id;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var result = await _mediator.Send(command);
            return NewResult(result);
        }


        //Put : Admin/Changeimg
        [HttpPut("ChangeImage")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeClientImage([FromForm] ChangeAdminImgURLCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            command.UserId = Id;
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        //Put : User/block
        [HttpPut("BlockUser/{userId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Block([FromRoute]Guid userId)
        {
            var result = await _mediator.Send(new BlockByAdminCommand(userId));
            return NewResult(result);
        }

    }
}
