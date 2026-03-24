using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Queries.DTOs
{
    public class MessageQueryDTO
    {

        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public string? SenderImgUrl { get; set; }
        public bool SenderIsActive { get; set; } = false;


        public int RoomId { get; set; }

    }
}
