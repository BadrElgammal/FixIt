using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Queries.DTOs
{
    public class MyRoomQueryDTO
    {
        public int RoomId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? LastMessage { get; set; }
        public DateTime? LastMessageAt { get; set; }

        public Guid TargetUserId { get; set; } = Guid.NewGuid();
        public string TargetUserName { get; set; }
        public string? TargetUserImgUrl { get; set; }
        public bool TargetUserIsActive { get; set; } = false;
    }
}
