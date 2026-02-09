using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.Entities
{
    public class ChatMessage
    {
        [Key]
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public ChatRoom Room { get; set; }
        [ForeignKey("sender")]
        public int SenderId { get; set; }
        public User Sender { get; set; }
    }
}
