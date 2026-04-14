using FixIt.Core.Bases;
using FixIt.Core.Features.Chat.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Queries.Models
{
    public class GetRoomMessagesQuery : IRequest<Response<List<MessageQueryDTO>>>
    {
        public Guid CurrentUserId { get; set; }
        public string Role {  get; set; }
        public int RoomId { get; set; }

        public GetRoomMessagesQuery(Guid CurrentUserId , string role , int roomId)
        {
            CurrentUserId = CurrentUserId;
            Role = role;
            RoomId = roomId;
        }
    }
}
