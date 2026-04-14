using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Chat.Queries.DTOs;
using FixIt.Core.Features.Chat.Queries.Models;
using FixIt.Core.Wrapper;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Queries.Handlers
{
    public class ChatQueryHandler : ResponseHandler,
                        IRequestHandler<GetMyRoomsQuery, Response<List<MyRoomQueryDTO>>>,
                        IRequestHandler<GetRoomMessagesQuery, Response<List<MessageQueryDTO>>>,
                        IRequestHandler<GetAllRoomsQuery, PaginatedResult<AllRoomsQueryDTO>>

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
            if ((request.CurrentUserId != room.CurrentUserId && request.CurrentUserId != room.TargetUserId) && (request.Role.ToLower() != "admin"))
                return BadRequest<List<MessageQueryDTO>>("عفوا ليس لديك صلاحيه");
            var messages = await _chatService.GetMessages(request.RoomId);
            var messagesmMpper = _mapper.Map<List<MessageQueryDTO>>(messages);
            return Success(messagesmMpper);
        }

        public async Task<PaginatedResult<AllRoomsQueryDTO>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ChatRoom, AllRoomsQueryDTO>> expression = e => new AllRoomsQueryDTO(e.RoomId, e.CreatedAt, e.LastMessage, e.LastMessageAt, e.CurrentUserId, e.CurrentUser.FullName, e.CurrentUser.ImgUrl, e.CurrentUser.IsActive, e.TargetUserId, e.TargetUser.FullName, e.TargetUser.ImgUrl, e.TargetUser.IsActive);
            var query = _chatService.GetAllRooms();
            var paginatedList = await query.Select(expression).ToPaginatedListAsync(request.pageNum , request.pageSize);
            return paginatedList;
        }
    }
}
