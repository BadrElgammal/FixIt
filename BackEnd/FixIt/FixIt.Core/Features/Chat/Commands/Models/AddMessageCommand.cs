using FixIt.Core.Bases;
using FixIt.Core.Features.Chat.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Commands.Models
{
    public class AddMessageCommand : IRequest<Response<MessageQueryDTO>>
    {
        public int RoomId { get; set; }
        public string Message { get; set; }
        public Guid SenderId { get; set; }
        public AddMessageCommand(int roomId, string message, Guid senderId)
        {
            RoomId = roomId;
            Message = message;
            SenderId = senderId;
        }
    }
}
