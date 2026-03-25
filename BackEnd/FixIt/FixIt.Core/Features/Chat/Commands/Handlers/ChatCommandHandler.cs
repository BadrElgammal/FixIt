using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Chat.Commands.Models;
using FixIt.Core.Features.Chat.Queries.DTOs;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using FixIt.Service.Services;
using MediatR;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Commands.Handlers
{
    public class ChatCommandHandler : ResponseHandler,
                        IRequestHandler<CreateOrGetRoomCommand, Response<int>>,
                        IRequestHandler<AddMessageCommand, Response<MessageQueryDTO>>,
                        IRequestHandler<SetUserActiveCommand, Response<bool>>,
                        IRequestHandler<MarkMessagesAsReadCommand, bool>

    {
        private readonly IMapper _mapper;
        private readonly IChatService _chatService;

        public ChatCommandHandler(IMapper mapper, IChatService chatService)
        {
            _mapper = mapper;
            _chatService = chatService;
        }


        public async Task<Response<int>> Handle(CreateOrGetRoomCommand request, CancellationToken cancellationToken)
        {
            if (request.currentUserId == request.targetUserId)
                return BadRequest<int>("متكلمش نفسك يصحبى ");
            var roomId = await _chatService.GetOrCreateRoom(request.currentUserId , request.targetUserId);
            return Success(roomId);
        }

        public async Task<Response<MessageQueryDTO>> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var room = await _chatService.GetRoomByRoomId(request.RoomId);
            if (request.SenderId != room.CurrentUserId && request.SenderId != room.TargetUserId)
                return BadRequest<MessageQueryDTO>("عفوا ليس لديك صلاحيه");

            var Message = new ChatMessage
            {
                MessageText = request.Message,
                RoomId = request.RoomId,
                SenderId = request.SenderId,
                ReciverId = request.SenderId == room.CurrentUserId ? room.TargetUserId : room.CurrentUserId
            };

            if(room != null)
            {
                room.LastMessage = request.Message;
                room.LastMessageAt = DateTime.UtcNow;
                await _chatService.UpdateRoom(room);
            }
            await _chatService.AddMessage(Message);



            var sender = _chatService.Find(u => u.UserId == request.SenderId).FirstOrDefault();

            var messageDto = new MessageQueryDTO
            {
                MessageId = Message.MessageId,
                MessageText = Message.MessageText,
                IsRead = Message.IsRead,
                CreatedAt = Message.CreatedAt,
                SenderId = Message.SenderId,
                SenderName = sender.FullName, 
                SenderImgUrl = sender.ImgUrl,
                SenderIsActive = true, 
                RoomId = Message.RoomId,
                ReceiverId = Message.ReciverId
            };
            return Success(messageDto);
        }

        public async Task<Response<bool>> Handle(SetUserActiveCommand request, CancellationToken cancellationToken)
        {
            var user = _chatService.Find(u => u.UserId == request.UserId).FirstOrDefault();
            if (user == null)
                return Success(false);
            user.IsActive = request.IsActive;
            if(!request.IsActive)
                user.LastLogin = DateTime.UtcNow;

            var result = await _chatService.UpdateUser(user);
            if (result != "success") return BadRequest<bool>("لم يتم تحديث بيانات حاله اليوزر");
            return Success(true);
        }

        public async Task<bool> Handle(MarkMessagesAsReadCommand request, CancellationToken cancellationToken)
        {
            var message = await _chatService.GetMessages(request.RoomId);
            if (!message.Any())
                return true;
            foreach(var msg in message)
            {
                if( msg.RoomId == request.RoomId && msg.ReciverId == request.UserId && msg.IsRead == false)
                    msg.IsRead = true;
            }
            var result = await _chatService.UpdateRangeMessages(message);
            if (result != "success") return false;
            return true;
        }
    }
}
