using FixIt.Domain.Entities;

namespace FixIt.Service.Abstracts
{
    public interface IPaymobService
    {
        // charge =>

        Task<(Payment PaymentRecord, string RedirectUrl)> ProcessPaymentAsync(Guid paymentId, string paymentMethod);
        Task<Payment> UpdateOrderSuccess(string GatewayRef);
        Task<Payment> UpdateOrderFailed(string GatewayRef);
        string ComputeHmacSHA512(string data, string secret);

        //withdraw =>
        Task<bool> RequestWithdrawalAsync(Guid userId, decimal amount, string method);
        Task<bool> ProcessWithdrawalRequestAsync(Guid requestId, bool approve);


    }
}