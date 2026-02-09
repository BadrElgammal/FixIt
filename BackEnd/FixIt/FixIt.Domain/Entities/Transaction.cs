using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string RefType { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("FromWallet")]
        public int FromWalletId { get; set; }
        public Wallet FromWallet { get; set; }
        [ForeignKey("ToWallet")]
        public int ToWalletId { get; set; }
        public Wallet ToWallet { get; set; }
        [ForeignKey("Request")]
        public int? RequestId { get; set; }
        public ServiceRequest? Request { get; set; }
    }
}
