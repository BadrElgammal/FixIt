using FixIt.API.Base;
using FixIt.API.SignalR;
using FixIt.Core.Features.Chat.Commands.Models;
using FixIt.Core.Features.Chat.Queries.Models;
using FixIt.Core.Features.Service.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FixIt.API.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : AppController
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IMediator mediator , IHubContext<ChatHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyChats()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new GetMyRoomsQuery(Id));
            return NewResult(result);
        }

        [HttpPost("room/{targetUserId}")]
        [Authorize]
        public async Task<IActionResult> CreateOrGetRoom([FromBody] Guid targetUserId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var roomId = await _mediator.Send(new CreateOrGetRoomCommand(Id, targetUserId));
            return NewResult(roomId);
        }


        [HttpGet("room/{roomId}")]
        [Authorize]
        public async Task<IActionResult> GetRoomMessages([FromBody] int roomId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new GetRoomMessagesQuery(Id, roomId));
            return NewResult(result);
        }


        [HttpPost("room/{roomId}/messages")]
        [Authorize]
        public async Task<IActionResult> SendMessage([FromRoute] int roomId, [FromBody] string messageText)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (Guid.TryParse(userId, out Guid senderId))
            {
                var result = await _mediator.Send(new AddMessageCommand(roomId, messageText, senderId));

                if (result.Succeeded)
                {
                    await _hubContext.Clients.Group(roomId.ToString())
                                     .SendAsync("ReceiveMessage", result.Data);

                    return Ok(result);
                }
            }

            return BadRequest("مشكلة في بيانات المستخدم");
        }
    }
}
