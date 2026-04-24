using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Payment.Queries.DTOs
{
    public class GetAllDepositQueryDTO
    {
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Gateway { get; set; }
        public string GatewayRef { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReleasedAt { get; set; }
        public Guid WalletId { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string? ImgUrl { get; set; }
        public string Email { get; set; }

        public GetAllDepositQueryDTO(Guid paymentId, decimal amount, string status, string gateway, string gatewayRef, DateTime createdAt, DateTime? releasedAt, Guid walletId, Guid userId, string fullName, string? imgUrl, string email)
        {
            PaymentId = paymentId;
            Amount = amount;
            Status = status;
            Gateway = gateway;
            GatewayRef = gatewayRef;
            CreatedAt = createdAt;
            ReleasedAt = releasedAt;
            WalletId = walletId;
            UserId = userId;
            FullName = fullName;
            ImgUrl = imgUrl;
            Email = email;
        }
    }
}
