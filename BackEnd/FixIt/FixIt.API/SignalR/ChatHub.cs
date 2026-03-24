using FixIt.Core.Features.Chat.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FixIt.API.SignalR
{
    [Authorize]
    public class ChatHub:Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        // اليوزر أول ما يضغط على الشات في الـ Angular هينادي الميثود دي
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        // اليوزر لما يقفل الشات أو يغير الروم، يفضل نخرجه من الجروب
        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }

        // الميثود المسئولة عن إرسال الرسالة
        //public async Task SendMessage(int roomId, Guid targetUserId, string message)
        //{
        //    var senderId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    // 1. الحفظ في الداتابيز الأول (أهم حاجة)
        //    // var savedMessage = await _mediator.Send(new AddMessageCommand(...));

        //    // 2. بنبعت الرسالة جوه الروم (عشان لو الطرف التاني فاتح الشات حالياً)
        //    await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", new
        //    {
        //        roomId = roomId,
        //        senderId = senderId,
        //        message = message
        //    });

        //    // 3. بنبعت إشعار لليوزر نفسه (عشان لو هو أونلاين بس مش فاتح الروم)
        //    await Clients.User(targetUserId.ToString()).SendAsync("ReceiveNotification", new
        //    {
        //        roomId = roomId,
        //        senderId = senderId,
        //        message = "لديك رسالة جديدة"
        //    });
        //}

        public async Task SendMessage(int roomId, string messageText)
        {
            var senderIdStr = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(senderIdStr, out Guid senderId))
            {
                var command = new AddMessageCommand(roomId,messageText, senderId);
                var result = await _mediator.Send(command);

                if (result.Succeeded)
                {
                    await Clients.Group(roomId.ToString())
                                     .SendAsync("ReceiveMessage", result.Data); 
                }
            }
        }
    }
}
