using FixIt.Domain.Enum;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public NotificationType? NotificationType { get; set; } //Message or Order or System
        public string? RelatedEntityId { get; set; }


        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }

}
