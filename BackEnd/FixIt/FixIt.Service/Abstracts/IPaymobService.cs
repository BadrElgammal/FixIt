using FixIt.Domain.Entities;

namespace FixIt.Service.Abstracts
{
    public interface IPaymobService
    {
        // Replace Enrollment with your entity class

        Task<(Payment PaymentRecord, string RedirectUrl)> ProcessPaymentAsync(Guid paymentId, string paymentMethod);
        Task<Payment> UpdateOrderSuccess(string GatewayRef);
        Task<Payment> UpdateOrderFailed(string GatewayRef);
        string ComputeHmacSHA512(string data, string secret);



    }
}