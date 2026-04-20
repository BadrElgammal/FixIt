using FixIt.Domain.Entities;
using FixIt.Infrastructure.Context;
using FixIt.Service.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymobService _paymobService;
        private readonly IConfiguration _configuration;
        private readonly FIXITDbContext _db;

        public PaymentController(IPaymobService paymobService, FIXITDbContext db, IConfiguration configuration)
        {
            _paymobService = paymobService;
            _configuration = configuration;
            _db = db;
        }


        // -------------------------------------------------- ChargeWallet --------------------------------------------------

        /// <summary>
        /// بتبدأ عملية شحن المحفظة وبترجع رابط الدفع للفرونت إند
        /// </summary>
        [Authorize]
        [HttpPost("charge-wallet")]
        public async Task<IActionResult> ChargeWallet(
           [FromQuery] decimal amount,
           [FromQuery] string paymentMethod)
        {
            if (amount <= 0)
                return BadRequest("Amount must be greater than zero.");

            if (string.IsNullOrWhiteSpace(paymentMethod))
                return BadRequest("Payment method is required.");

            // 1. نجيب الـ ID بتاع اليوزر اللي عامل Login
            var userIdString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized("User not authenticated.");

            try
            {
                // 2. نجيب محفظة اليوزر من الداتا بيز
                var wallet = await _db.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);

                if (wallet == null)
                    return NotFound("Wallet not found for this user.");

                // 3. ننشئ عملية الدفع (Payment) ونحفظها
                var payment = new Payment
                {
                    PaymentId = Guid.NewGuid(),
                    Amount = amount,
                    Status = "Initiated",
                    WalletId = wallet.Id,
                    CreatedAt = DateTime.Now,

                    Gateway = "Paymob",
                    GatewayRef = "Pending"
                };

                await _db.Payments.AddAsync(payment);
                await _db.SaveChangesAsync();

                // 4. نبعت الطلب لخدمة بيموب
                if (paymentMethod.Equals("card", StringComparison.OrdinalIgnoreCase) ||
                    paymentMethod.Equals("wallet", StringComparison.OrdinalIgnoreCase))
                {
                    var (paymentRecord, redirectUrl) = await _paymobService.ProcessPaymentAsync(payment.PaymentId, paymentMethod);
                    return Ok(new { RedirectUrl = redirectUrl, PaymentId = payment.PaymentId });
                }
                else
                {
                    return BadRequest("Invalid payment method. Supported methods are 'card' and 'wallet'.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing payment: {ex.Message}");
            }
        }

        /// <summary>
        /// رابط إعادة توجيه العميل بعد الدفع (Front-end Redirect)
        /// </summary>
        [HttpGet("callback")]
        public async Task<IActionResult> CallbackAsync()
        {
            var query = Request.Query;

            string[] fields = new[]
            {
                "amount_cents", "created_at", "currency", "error_occured", "has_parent_transaction",
                "id", "integration_id", "is_3d_secure", "is_auth", "is_capture", "is_refunded",
                "is_standalone_payment", "is_voided", "order", "owner", "pending",
                "source_data.pan", "source_data.sub_type", "source_data.type", "success"
            };

            var concatenated = new StringBuilder();
            foreach (var field in fields)
            {
                if (query.TryGetValue(field, out var value))
                {
                    concatenated.Append(value);
                }
                else
                {
                    return BadRequest($"Missing expected field: {field}");
                }
            }

            string receivedHmac = query["hmac"];
            string calculatedHmac = _paymobService.ComputeHmacSHA512(concatenated.ToString(), Environment.GetEnvironmentVariable("HMAC")!);//_configuration["Paymob:HMAC"]

            if (receivedHmac.Equals(calculatedHmac, StringComparison.OrdinalIgnoreCase))
            {
                bool.TryParse(query["success"], out bool isSuccess);

                if (isSuccess)
                {
                    return Content("<h2>Payment Successful! Your wallet has been charged.</h2>", "text/html");
                }

                return Content("<h2>Payment Failed! Please try again.</h2>", "text/html");
            }

            return Content("<h2>Security verification failed!</h2>", "text/html");
        }

        /// <summary>
        /// Webhook: بيموب بتكلم السيرفر بتاعك هنا في الخلفية
        /// </summary>
        [HttpPost("server-callback")]
        public async Task<IActionResult> ServerCallback([FromBody] JsonElement payload)
        {
            try
            {
                string receivedHmac = Request.Query["hmac"];
                string secret = Environment.GetEnvironmentVariable("HMAC")!;//_configuration["Paymob:HMAC"];

                if (!payload.TryGetProperty("obj", out var obj))
                    return BadRequest("Missing 'obj' in payload.");

                string[] fields = new[]
                {
                    "amount_cents", "created_at", "currency", "error_occured", "has_parent_transaction",
                    "id", "integration_id", "is_3d_secure", "is_auth", "is_capture", "is_refunded",
                    "is_standalone_payment", "is_voided", "order.id", "owner", "pending",
                    "source_data.pan", "source_data.sub_type", "source_data.type", "success"
                };

                var concatenated = new StringBuilder();
                foreach (var field in fields)
                {
                    string[] parts = field.Split('.');
                    JsonElement current = obj;
                    bool found = true;
                    foreach (var part in parts)
                    {
                        if (current.ValueKind == JsonValueKind.Object && current.TryGetProperty(part, out var next))
                            current = next;
                        else
                        {
                            found = false;
                            break;
                        }
                    }

                    if (!found || current.ValueKind == JsonValueKind.Null)
                    {
                        concatenated.Append("");
                    }
                    else if (current.ValueKind == JsonValueKind.True || current.ValueKind == JsonValueKind.False)
                    {
                        concatenated.Append(current.GetBoolean() ? "true" : "false");
                    }
                    else
                    {
                        concatenated.Append(current.ToString());
                    }
                }

                string calculatedHmac = _paymobService.ComputeHmacSHA512(concatenated.ToString(), secret);

                if (!receivedHmac.Equals(calculatedHmac, StringComparison.OrdinalIgnoreCase))
                    return Unauthorized("Invalid HMAC");

                string merchantOrderId = null;
                if (obj.TryGetProperty("order", out var order) &&
                    order.TryGetProperty("merchant_order_id", out var merchantOrderIdElement) &&
                    merchantOrderIdElement.ValueKind != JsonValueKind.Null)
                {
                    merchantOrderId = merchantOrderIdElement.ToString();
                }

                bool isSuccess = obj.TryGetProperty("success", out var successElement) && successElement.GetBoolean();

                if (!string.IsNullOrEmpty(merchantOrderId))
                {
                    if (isSuccess)
                        await _paymobService.UpdateOrderSuccess(merchantOrderId);
                    else
                        await _paymobService.UpdateOrderFailed(merchantOrderId);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing server callback: {ex.Message}");
            }
        }

        // -----------------------------------------------------------------------------------------------------------------------------



        // -------------------------------------------------- WithdrawRequest --------------------------------------------------


        /// <summary>
        /// المستخدم بيطلب سحب مبلغ من محفظته
        /// </summary>
        [Authorize]
        [HttpPost("withdraw-request")]
        public async Task<IActionResult> RequestWithdraw([FromQuery] decimal amount, [FromQuery] string method)
        {
            if (amount <= 0) return BadRequest("المبلغ يجب أن يكون أكبر من صفر");

            var userIdString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out Guid userId)) return Unauthorized();

            var result = await _paymobService.RequestWithdrawalAsync(userId, amount, method);

            if (result) return Ok("تم تقديم طلب السحب بنجاح وحجز المبلغ من رصيدك.");
            return BadRequest("رصيدك غير كافٍ لإتمام العملية.");
        }

        /// <summary>
        /// للأدمن فقط: الموافقة أو الرفض على طلب السحب
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPost("admin/process-withdraw/{requestId}")]
        public async Task<IActionResult> ProcessWithdraw(Guid requestId, [FromQuery] bool approve)
        {
            var result = await _paymobService.ProcessWithdrawalRequestAsync(requestId, approve);

            if (result) return Ok($"تمت معالجة الطلب بـ {(approve ? "الموافقة والدفع" : "الرفض وإعادة الرصيد")}.");
            return BadRequest("الطلب غير موجود أو تمت معالجته مسبقاً.");
        }

        /// <summary>
        /// عرض كل طلبات السحب للأدمن لمراجعتها
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpGet("admin/withdraw-requests")]
        public async Task<IActionResult> GetAllWithdrawRequests()
        {
            var requests = await _db.WithdrawRequests
                .Include(r => r.Wallet.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            return Ok(requests);
        }

        // -----------------------------------------------------------------------------------------------------------------------------

    }
}

