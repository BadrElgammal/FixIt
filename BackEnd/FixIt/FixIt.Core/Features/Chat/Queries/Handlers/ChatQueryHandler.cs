using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Chat.Queries.DTOs;
using FixIt.Core.Features.Chat.Queries.Models;
using FixIt.Service.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Queries.Handlers
{
    public class ChatQueryHandler : ResponseHandler,
                        IRequestHandler<GetMyRoomsQuery, Response<List<MyRoomQueryDTO>>>,
                        IRequestHandler<GetRoomMessagesQuery, Response<List<MessageQueryDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IChatService _chatService;

        public ChatQueryHandler(IMapper mapper, IChatService chatService)
        {
            _mapper = mapper;
            _chatService = chatService;
        }

        public async Task<Response<List<MyRoomQueryDTO>>> Handle(GetMyRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _chatService.GetUserRooms(request.CurrentUserId);
            if(!rooms.Any()) 
                return NotFound<List<MyRoomQueryDTO>>("لا توجد دردشات بعد");
            var roomsMapper = _mapper.Map<List<MyRoomQueryDTO>>(rooms);
            return Success(roomsMapper);
        }

        public async Task<Response<List<MessageQueryDTO>>> Handle(GetRoomMessagesQuery request, CancellationToken cancellationToken)
        {
            var room = await _chatService.GetRoomByRoomId(request.RoomId);
            if (request.CurrentUserId != room.CurrentUserId && request.CurrentUserId != room.TargetUserId)
                return BadRequest<List<MessageQueryDTO>>("عفوا ليس لديك صلاحيه");
            var messages = await _chatService.GetMessages(request.RoomId);
            var messagesmMpper = _mapper.Map<List<MessageQueryDTO>>(messages);
            return Success(messagesmMpper);
        }
    }
}
