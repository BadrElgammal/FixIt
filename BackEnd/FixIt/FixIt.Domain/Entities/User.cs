using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(100)]
        [MinLength(3)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$")]
        public string Phone { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        public string? ImgUrl { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }


        [ForeignKey("Wallet")]
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }

        public ICollection<ServiceRequest>? SentRequests { get; set; } = new List<ServiceRequest>();
        public ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public ICollection<ChatRoom>? ClientChatRooms { get; set; } = new List<ChatRoom>();
        public ICollection<ChatRoom>? WorkerChatRooms { get; set; } = new List<ChatRoom>();
        public ICollection<ChatMessage>? Messages { get; set; } = new List<ChatMessage>();
        public ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>();
    }
}
