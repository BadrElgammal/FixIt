using FixIt.Domain.Entities;
using FixIt.Infrastructure.Context;
using FixIt.Service.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace FixIt.Service.Services
{
    internal class PaymobService : IPaymobService
    {



        private readonly IConfiguration _configuration;
        private readonly FIXITDbContext _context;

        public PaymobService(IConfiguration configuration, FIXITDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<(Payment PaymentRecord, string RedirectUrl)> ProcessPaymentAsync(Guid paymentId, string paymentMethod)
        {
            // 1. هنجيب عملية الدفع والمحفظة والمستخدم المرتبط بيها
            var payment = await _context.Payments
                .Include(p => p.Wallet)
                .ThenInclude(w => w.User)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);

            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {paymentId} not found.");

            var user = payment.Wallet?.User;
            if (user == null)
                throw new InvalidOperationException("User associated with this wallet not found.");

            var httpClient = new HttpClient();

            //string secretKey = _configuration["Paymob:SecretKey"] ?? throw new ArgumentException("Paymob secret key not configured");
            //string publicKey = _configuration["Paymob:PublicKey"] ?? throw new ArgumentException("Paymob public key not configured");
            string secretKey = Environment.GetEnvironmentVariable("SECRETKEY")!;
            string publicKey = Environment.GetEnvironmentVariable("PUBLICKEY")!;

            // 2. تحويل المبلغ لقروش
            var amountCents = (int)(payment.Amount * 100);

            // 3. تجهيز بيانات العميل (تأكد إن الخصائص دي موجودة في كلاس User عندك)

            var nameParts = (user.FullName ?? "FixIt User").Split(' ');
            var FirstName = nameParts[0];
            var LastName = nameParts.Length > 1 ? nameParts[1] : "Customer";

            var billingData = new
            {
                first_name = FirstName,
                last_name = LastName,
                phone_number = user.Phone ?? "01000000000",
                email = user.Email ?? "user@fixit.com",
                apartment = "N/A",
                street = "N/A",
                building = "N/A",
                country = "EG",
                floor = "N/A",
                state = "N/A",
                city = "N/A"
            };

            var integrationId = int.Parse(DetermineIntegrationId(paymentMethod));
            string specialReference = payment.PaymentId.ToString(); // استخدمنا الـ Guid كمرجع فريد

            // 4. تجهيز الطلب لبيموب
            var payload = new
            {
                amount = amountCents,
                currency = "EGP",
                payment_methods = new[] { integrationId },
                billing_data = billingData,
                items = new[]
                {
                    new
                    {
                        name = "Wallet Charge",
                        amount = amountCents,
                        description = $"Charging wallet for user {user.UserId}",
                        quantity = 1
                    }
                },
                customer = new
                {
                    full_name = billingData.first_name,
                    last_name = billingData.last_name,
                    email = billingData.email
                },
                special_reference = specialReference,
                expiration = 3600, // صالح لمدة ساعة
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://accept.paymob.com/v1/intention/");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Token", secretKey);
            requestMessage.Content = JsonContent.Create(payload);

            var response = await httpClient.SendAsync(requestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Paymob API failed: {responseContent}");

            var resultJson = JsonDocument.Parse(responseContent);
            var clientSecret = resultJson.RootElement.GetProperty("client_secret").GetString();

            // 5. تحديث بيانات الدفع في الداتا بيز
            payment.Gateway = "Paymob";
            payment.GatewayRef = specialReference;
            payment.Status = "Pending";

            await _context.SaveChangesAsync();

            // 6. إنشاء الرابط اللي هيرجع للـ Front-end
            string redirectUrl = $"https://accept.paymob.com/unifiedcheckout/?publicKey={publicKey}&clientSecret={clientSecret}";

            return (payment, redirectUrl);
        }

        private string DetermineIntegrationId(string paymentMethod)
        {
            return paymentMethod?.ToLower() switch
            {
                //"card" => _configuration["Paymob:CardIntegrationId"] ?? throw new ArgumentException("Card ID missing"),
                "card" => Environment.GetEnvironmentVariable("CARDINTEGRATIONID")!,
                "wallet" => _configuration["Paymob:MobileIntegrationId"] ?? throw new ArgumentException("Wallet ID missing"),
                _ => throw new ArgumentException($"Invalid payment method: {paymentMethod}")
            };
        }

        public async Task<Payment> UpdateOrderSuccess(string specialReference)
        {
            // البحث عن عملية الدفع باستخدام الـ GatewayRef
            var payment = await _context.Payments
                .Include(p => p.Wallet)
                .FirstOrDefaultAsync(p => p.GatewayRef == specialReference);

            if (payment == null)
                throw new KeyNotFoundException("Payment not found.");

            // تحديث حالة الدفع والمحفظة
            if (payment.Status != "Success") // عشان لو بيموب بعتت الرد مرتين
            {
                payment.Status = "Success";
                payment.ReleasedAt = DateTime.Now;

                // تسميع الفلوس في المحفظة
                payment.Wallet.Balance += payment.Amount;
                payment.Wallet.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }

            return payment;
        }

        public async Task<Payment> UpdateOrderFailed(string specialReference)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.GatewayRef == specialReference);

            if (payment == null)
                throw new KeyNotFoundException("Payment not found.");

            payment.Status = "Failed";
            await _context.SaveChangesAsync();

            return payment;
        }

        public string ComputeHmacSHA512(string data, string secret)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secret);
            var dataBytes = Encoding.UTF8.GetBytes(data);

            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hash = hmac.ComputeHash(dataBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }


    }
}