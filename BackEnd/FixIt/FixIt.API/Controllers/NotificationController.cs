using FixIt.API.Base;
using FixIt.API.SignalR;
using FixIt.Core.Features.Chat.Queries.Models;
using FixIt.Core.Features.Notifications.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : AppController
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<ChatHub> _hubContext;

        public NotificationController(IMediator mediator, IHubContext<ChatHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpGet("MyNotifications")]
        [Authorize]
        public  async Task<IActionResult> GetAllNotifications()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var result = await _mediator.Send(new GetAllNotificationsQuery(Id));
            return NewResult(result);
        }

    }
}
