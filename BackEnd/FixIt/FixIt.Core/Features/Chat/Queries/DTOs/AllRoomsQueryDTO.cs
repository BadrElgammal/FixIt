using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Queries.DTOs
{
    public class AllRoomsQueryDTO
    {
        public int RoomId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? LastMessage { get; set; }
        public DateTime? LastMessageAt { get; set; }

        public Guid CurrentUserId { get; set; }
        public string CurrentUserName { get; set; }
        public string? CurrentUserImgUrl { get; set; }
        public bool CurrentUserIsActive { get; set; } = false;
        public Guid TargetUserId { get; set; } = Guid.NewGuid();
        public string TargetUserName { get; set; }
        public string? TargetUserImgUrl { get; set; }
        public bool TargetUserIsActive { get; set; } = false;
       

        public AllRoomsQueryDTO(int roomId, DateTime createdAt, string? lastMessage, DateTime? lastMessageAt, Guid currentUserId, string currentUserName, string? currentUserImgUrl, bool currentUserIsActive, Guid targetUserId, string targetUserName, string? targetUserImgUrl, bool targetUserIsActive)
        {
            RoomId = roomId;
            CreatedAt = createdAt;
            LastMessage = lastMessage;
            LastMessageAt = lastMessageAt;
            TargetUserId = targetUserId;
            TargetUserName = targetUserName;
            TargetUserImgUrl = targetUserImgUrl;
            TargetUserIsActive = targetUserIsActive;
            CurrentUserId = currentUserId;
            CurrentUserName = currentUserName;
            CurrentUserImgUrl = currentUserImgUrl;
            CurrentUserIsActive = currentUserIsActive;
        }
    }
}
