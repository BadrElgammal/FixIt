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
                        IRequestHandler<AddMessageCommand, Response<MessageQueryDTO>>

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
                SenderId = request.SenderId
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
                RoomId = Message.RoomId
            };
            return Success(messageDto);
        }
    }
}
