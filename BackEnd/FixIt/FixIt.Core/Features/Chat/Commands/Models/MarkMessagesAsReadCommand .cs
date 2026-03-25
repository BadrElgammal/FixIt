using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Commands.Models
{
    public class MarkMessagesAsReadCommand : IRequest<bool>
    {
        public int RoomId { get; set; }
        public Guid UserId { get; set; }

        public MarkMessagesAsReadCommand(int roomId, Guid userId)
        {
            RoomId = roomId;
            UserId = userId;
        }
    }
}
