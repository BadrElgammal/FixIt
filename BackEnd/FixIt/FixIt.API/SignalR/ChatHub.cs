using FixIt.Core.Features.Chat.Commands.Models;
using FixIt.Core.Features.Notifications.Commands.Models; // 👈 مسار الإشعارات
using FixIt.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace FixIt.API.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        // 👈 ضفنا ده عشان نعد اليوزر فاتح كام Tab ومندمرش الداتابيز
        private static readonly ConcurrentDictionary<Guid, int> OnlineUsers = new();
        // Key: UserId, Value: HashSet of RoomIds (عشان لو فاتح من موبايل ولاب توب مع بعض)
        private static readonly ConcurrentDictionary<Guid, HashSet<int>> UserActiveRooms = new();

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task JoinRoom(int roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());

            var userIdStr = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdStr, out Guid userId))
            {
                // بنسجل إن اليوزر ده دخل الروم دي
                UserActiveRooms.AddOrUpdate(userId,
                    new HashSet<int> { roomId },
                    (key, existingRooms) => { existingRooms.Add(roomId); return existingRooms; });
            }
        }

        public async Task LeaveRoom(int roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());

            var userIdStr = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdStr, out Guid userId))
            {
                // بنشيله من الروم دي لما يقفلها
                if (UserActiveRooms.TryGetValue(userId, out var rooms))
                {
                    rooms.Remove(roomId);
                }
            }
        }

        public async Task SendMessage(int roomId, string messageText, Guid targetUserId)
        {
            var senderIdStr = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(senderIdStr, out Guid senderId)) return;

            // 1. نسيف الرسالة في جدول الرسايل ونبعتها في الروم (ده الأساسي)
            var command = new AddMessageCommand(roomId, messageText, senderId);
            var result = await _mediator.Send(command);

            await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", result.Data);

            // 🔥 اللوجيك بتاع الإشعارات 🔥
            // بنسأل: هل اليوزر التاني أونلاين وفاتح الروم دي تحديداً دلوقتي؟
            bool isTargetInRoom = UserActiveRooms.ContainsKey(targetUserId) &&
                                  UserActiveRooms[targetUserId].Contains(roomId);

            // لو هو مش فاتح الروم (أو قافل الأبلكيشن خالص)
            if (!isTargetInRoom)
            {
                // أ. نكريت إشعار في الداتابيز عشان لما يفتح يلاقيه
                await _mediator.Send(new AddNotificationCommand(
                    UserId: targetUserId, // خلينا اسم الباراميترز سمول عشان يطابق الـ Constructor
                    title: "رسالة جديدة",
                    message: "لديك رسالة جديدة",
                    notificationType: NotificationType.Message,
                    relatedEntityId: roomId.ToString()
                ));

                // ب. نبعت الإشعار لحظياً لو هو فاتح الموقع بس بيتصفح صفحة تانية
                await Clients.User(targetUserId.ToString())
                             .SendAsync("ReceiveNotification", new
                             {
                                 roomId = roomId,
                                 message = "لديك رسالة جديدة"
                             });
            }
        }

        // ✅ تحديث حالة الأونلاين (مع حماية الـ Tabs)
        public override async Task OnConnectedAsync()
        {
            var userIdStr = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdStr, out Guid id))
            {
                // بنزود العداد بتاع التابات
                OnlineUsers.AddOrUpdate(id, 1, (_, count) => count + 1);

                // لو دي أول تاب يفتحها، نخليه أونلاين في الداتابيز
                if (OnlineUsers[id] == 1)
                {
                    await _mediator.Send(new SetUserActiveCommand(id, true));
                }
            }

            await base.OnConnectedAsync();
        }

        // دمجنا الاتنين OnDisconnectedAsync مع بعض
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userIdStr = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdStr, out Guid id))
            {
                // 1. لو النت فصل، نشيله من الرومز اللي متسجلة باسمه
                UserActiveRooms.TryRemove(id, out _);

                // 2. بننقص العداد بتاع التابات
                if (OnlineUsers.ContainsKey(id))
                {
                    OnlineUsers[id]--;

                    // لو قفل كل التابات خالص، نخليه أوفلاين
                    if (OnlineUsers[id] <= 0)
                    {
                        OnlineUsers.TryRemove(id, out _);
                        await _mediator.Send(new SetUserActiveCommand(id, false));
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        // ✅ علامة الـ Seen
        public async Task MarkAsRead(int roomId)
        {
            var userIdStr = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdStr, out Guid userId))
                return;

            var result = await _mediator.Send(new MarkMessagesAsReadCommand(roomId, userId));

            if (!result) // ممكن تحتاج تتشيك على result.Succeeded لو إنت مرجع Response 
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