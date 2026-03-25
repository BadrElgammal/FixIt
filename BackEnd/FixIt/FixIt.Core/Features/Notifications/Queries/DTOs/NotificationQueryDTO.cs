using FixIt.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Notifications.Queries.DTOs
{
    public class NotificationQueryDTO
    {
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public NotificationType? NotificationType { get; set; } 
        public string? RelatedEntityId { get; set; }
    }
}
