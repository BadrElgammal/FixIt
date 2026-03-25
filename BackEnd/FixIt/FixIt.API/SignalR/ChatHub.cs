using FixIt.Core.Features.Chat.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FixIt.API.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ✅ لما اليوزر يدخل روم
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

            // اختياري: تأكيد للفرونت
            await Clients.Caller.SendAsync("JoinedRoom", roomId);
        }

        // ✅ لما يخرج من الروم
        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);

            await Clients.Caller.SendAsync("LeftRoom", roomId);
        }

        // ✅ إرسال رسالة
        public async Task SendMessage(int roomId, string messageText)
        {
            var senderIdStr = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(senderIdStr, out Guid senderId))
            {
                await Clients.Caller.SendAsync("Error", "Invalid user");
                return;
            }

            var command = new AddMessageCommand(roomId, messageText, senderId);
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                await Clients.Caller.SendAsync("Error", result.Message);
                return;
            }

            // ✅ نبعت الرسالة لكل اللي في الروم
            await Clients.Group(roomId.ToString())
                         .SendAsync("ReceiveMessage", result.Data);

            // ✅ (اختياري مهم) إشعار لو حد مش فاتح الروم
            if (result.Data?.ReceiverId != null)
            {
                await Clients.User(result.Data.ReceiverId.ToString())
                             .SendAsync("ReceiveNotification", new
                             {
                                 roomId = roomId,
                                 message = "لديك رسالة جديدة"
                             });
            }
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userId, out Guid id))
            {
                await _mediator.Send(new SetUserActiveCommand(id, true));
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userId, out Guid id))
            {
                await _mediator.Send(new SetUserActiveCommand(id, false));
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task MarkAsRead(int roomId)
        {
            var userIdStr = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdStr, out Guid userId))
                return;

            var result = await _mediator.Send(new MarkMessagesAsReadCommand(roomId, userId));

            if (!result)
                return;

            await Clients.Group(roomId.ToString())
                .SendAsync("MessagesRead", new
                {
                    roomId = roomId,
                    readerId = userId
                });
        }
    }
}