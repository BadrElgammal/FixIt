using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.Entities
{
    public class ChatRoom
    {
        [Key]
        public int RoomId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastMessage { get; set; }
        public DateTime? LastMessageAt { get; set; }

        public int ClientId { get; set; }
        public User Client { get; set; }

        public int WorkerId { get; set; }
        public User Worker { get; set; }

        public ICollection<ChatMessage>? Messages { get; set; } = new List<ChatMessage>();
    }
}
