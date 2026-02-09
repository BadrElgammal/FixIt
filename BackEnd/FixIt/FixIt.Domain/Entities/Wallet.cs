using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.Entities
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string OwnerType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Transaction>? FromTransactions { get; set; } = new List<Transaction>();
        public ICollection<Transaction>? ToTransactions { get; set; } = new List<Transaction>();
        public ICollection<Payment>? Payments { get; set; } = new List<Payment>();
        public ICollection<WithdrawRequest>? WithdrawRequests { get; set; } = new List<WithdrawRequest>();
    }
}
