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
        public Guid TransactionId { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string RefType { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("FromWallet")]
        public Guid FromWalletId { get; set; }
        public Wallet FromWallet { get; set; }
        [ForeignKey("ToWallet")]
        public Guid ToWalletId { get; set; }
        public Wallet ToWallet { get; set; }
        [ForeignKey("Request")]
        public  Guid? RequestId { get; set; }
        public ServiceRequest? Request { get; set; }
    }
}
