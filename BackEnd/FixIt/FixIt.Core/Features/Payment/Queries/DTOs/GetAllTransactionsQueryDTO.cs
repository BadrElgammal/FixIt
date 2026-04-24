using FixIt.Domain.Entities;
using FixIt.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Payment.Queries.DTOs
{
    public class GetAllTransactionsQueryDTO
    {
        public Guid TransactionId { get; set; } 
        public decimal Amount { get; set; }
        public decimal? ServiceCommetion { get; set; } = decimal.Zero;
        public string TransactionType { get; set; }
        public string RefType { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid FromWalletId { get; set; }
        public Guid UserId1 { get; set; }
        public string User1FullName { get; set; }
        public string? User1ImgUrl { get; set; }

        public Guid ToWalletId { get; set; }
        public Guid UserId2 { get; set; }
        public string User2FullName { get; set; }
        public string? User2ImgUrl { get; set; }

        public Guid? ServiceRequestId { get; set; }

        public GetAllTransactionsQueryDTO(Guid transactionId, decimal amount, decimal? serviceCommetion, string transactionType, string refType, DateTime createdAt, Guid fromWalletId, Guid userId1, string user1FullName, string? user1ImgUrl, Guid toWalletId, Guid userId2, string user2FullName, string? user2ImgUrl, Guid? serviceRequestId)
        {
            TransactionId = transactionId;
            Amount = amount;
            ServiceCommetion = serviceCommetion;
            TransactionType = transactionType;
            RefType = refType;
            CreatedAt = createdAt;
            FromWalletId = fromWalletId;
            UserId1 = userId1;
            User1FullName = user1FullName;
            User1ImgUrl = user1ImgUrl;
            ToWalletId = toWalletId;
            UserId2 = userId2;
            User2FullName = user2FullName;
            User2ImgUrl = user2ImgUrl;
            ServiceRequestId = serviceRequestId;
        }
    }
}
