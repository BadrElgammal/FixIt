using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Payment.Queries.DTOs
{
    public class WithdrawRequestsQueryDTO
    {
        public Guid Id { get; set; } 
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public Guid WalletId { get; set; }
        public Guid UserId { get; set; } 
        public string FullName { get; set; }
        public string? ImgUrl { get; set; }
        public string Email { get; set; }

        public WithdrawRequestsQueryDTO(Guid id, decimal amount, string method, string status, DateTime createdAt, DateTime? paidAt, Guid walletId, Guid userId, string fullName, string? imgUrl, string email)
        {
            Id = id;
            Amount = amount;
            Method = method;
            Status = status;
            CreatedAt = createdAt;
            PaidAt = paidAt;
            WalletId = walletId;
            UserId = userId;
            FullName = fullName;
            ImgUrl = imgUrl;
            Email = email;
        }
    }
}
