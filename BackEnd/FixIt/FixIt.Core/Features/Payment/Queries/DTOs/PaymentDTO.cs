using FixIt.Domain.Enum;

namespace FixIt.Core.Features.Payment.Queries.DTOs
{

    public class PaymentDTO
    {
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Gateway { get; set; }
        public DateTime CreatedAt { get; set; }
        public PaymentOperationType PaymentType { get; set; }
    }
}
