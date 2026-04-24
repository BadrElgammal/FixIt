using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Payment.Queries.DTOs
{
    public class WalletQueryDTO
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public string OwnerType { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
    }
}
