using FixIt.Core.Features.Auth.DTOs;
using FixIt.Domain.Entities;
using FixIt.Infrastructure.Context;
using FixIt.Infrastructure.Migrations;
using FixIt.Service.Abstracts;
using FixIt.Service.Services;
using Microsoft.AspNetCore.Mvc;


namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly FIXITDbContext _context;
        private readonly JWTService _JwtService;
        private readonly IService<User> _UserService;
        private readonly IService<WorkerProfile> _WorkerService;
        private readonly IService<Wallet> _WalletService;
        private readonly IEmailService _emailService;
        public AccountController(
            FIXITDbContext context,
            JWTService JwtService,
            IService<User> UserService,
            IService<WorkerProfile> WorkerService,
            IService<Wallet> WalletService,
            IEmailService emailService)
        {
            _context = context;
            _JwtService = JwtService;
            _UserService = UserService;
            _WorkerService = WorkerService;
            _WalletService = WalletService;
            _emailService = emailService;
        }











        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_UserService.Find(u => u.Email == userData.Email).Any())
            {
                ModelState.AddModelError("Email", "هذا البريد الإلكتروني مستخدم بالفعل. يرجى استخدام بريد آخر.");
                return BadRequest(ModelState);
            }
            if (_UserService.Find(u => u.Phone == userData.Phone).Any())
            {
                ModelState.AddModelError("Phone", "رقم الهاتف مستخدم بالفعل. يرجى استخدام رقم اخر.");
                return BadRequest(ModelState);
            }
            if (userData.Password != userData.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "كلمة المرور وتأكيد كلمة المرور غير متطابقتين.");
                return BadRequest(ModelState);
            }


            var user = new User
            {
                FullName = userData.Name,
                Email = userData.Email,
                Phone = userData.Phone,
                City = userData.City,
                Role = userData.Role.ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userData.Password),

                // 👇 التعديلات الجديدة
                IsEmailConfirmed = false,
                ConfirmationToken = Guid.NewGuid().ToString() // توليد توكن عشوائي فريد
            };

            await _UserService.AddAsync(user);

            // ... (كود إضافة الـ Worker والـ Wallet زي ما هو) ...

            // 👇 إرسال إيميل التأكيد
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.UserId, token = user.ConfirmationToken }, Request.Scheme);

            var emailBody = $@"
    <div dir='rtl' style='text-align: right; font-family: Arial;'>
        <h3>مرحباً بك يا {user.FullName} في منصتنا!</h3>
        <p>شكراً لتسجيلك معنا. برجاء تأكيد حسابك من خلال الضغط على الرابط التالي:</p>
        <a href='{confirmationLink}' style='background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>تأكيد الحساب</a>
    </div>";

            await _emailService.SendEmailAsync(user.Email, "تأكيد حسابك الجديد", emailBody);


            if (userData.Role.ToLower() == "worker")
            {
                var worker = new WorkerProfile() { UserId = user.UserId };
                await _WorkerService.AddAsync(worker);
            }
            var wallet = new Wallet()
            {
                UserId = user.UserId,
                OwnerType = user.Role
            };
            await _WalletService.AddAsync(wallet);

            string StringToken = _JwtService.GenerateToken(user);

            // (ممكن ترجع رسالة تطلب منه يفحص الإيميل بدل ما ترجع الـ JWT على طول لو عايز تجبره يأكد الأول)
            return Ok(new { message = "تم التسجيل بنجاح. برجاء فحص بريدك الإلكتروني لتأكيد الحساب." });
        }







        [HttpPost("Login")]
        public IActionResult Login(LoginDTO userFromRequst)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var userFromDb = _context.Users.FirstOrDefault(u => u.Email == userFromRequst.Email && u.PasswordHash == userFromRequst.Password);
            var userFromDb = _UserService.Find(u => u.Email == userFromRequst.Email).FirstOrDefault();

            if (userFromDb == null)
            {
                ModelState.AddModelError("", "لا يوجد حساب بهذا البريد الإلكتروني أو نوع الحساب غير صحيح.");
                return BadRequest(ModelState);
            }
            bool validPassword = BCrypt.Net.BCrypt.Verify(userFromRequst.Password, userFromDb.PasswordHash);
            if (userFromDb == null || !validPassword)
            {
                ModelState.AddModelError("", "الايميل او كلمة المرور غير صحيحه");
                return BadRequest(ModelState);
            }
            // بعد التأكد من الباسورد
            if (!userFromDb.IsEmailConfirmed)
            {
                return BadRequest(new { message = "برجاء تأكيد بريدك الإلكتروني أولاً لتتمكن من تسجيل الدخول." });
            }

            string StringToken = _JwtService.GenerateToken(userFromDb);

            return Ok(new { token = StringToken });
        }




        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            // 1. البحث عن المستخدم بالـ ID
            // استخدم اسم الميثود الصحيح اللي في الـ UserService بتاعك (سواء GetById أو Find)
            var user = _UserService.Find(u => u.UserId == userId).FirstOrDefault();

            if (user == null)
            {
                return BadRequest("مستخدم غير موجود.");
            }

            // 2. التأكد من أن التوكن مطابق للتوكن المحفوظ في الداتا بيز
            if (user.ConfirmationToken != token)
            {
                return BadRequest("رابط التأكيد غير صالح أو منتهي الصلاحية.");
            }

            // 3. تحديث حالة الحساب وتفريغ التوكن
            user.IsEmailConfirmed = true;
            user.ConfirmationToken = null;

            await _UserService.UpdateAsync(user);

            // 4. إرجاع رسالة نجاح
            return Ok(new { message = "تم تأكيد بريدك الإلكتروني بنجاح! يمكنك الآن تسجيل الدخول." });
        }




        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO dto)
        {
            var user = _UserService.Find(u => u.Email == dto.Email).FirstOrDefault();
            if (user == null) return Ok(new { message = "إذا كان الحساب موجوداً، فستصلك رسالة بريد إلكتروني." });

            // توليد توكن جديد وحفظه
            user.ConfirmationToken = Guid.NewGuid().ToString();
            await _UserService.UpdateAsync(user);

            // بناء اللينك (هنا بنفترض إن عندك صفحة في الفرونت اسمها reset-password)
            var resetLink = $"https://fixitapi.runasp.net/reset-password?email={user.Email}&token={user.ConfirmationToken}";

            var body = $@"<h3>إعادة تعيين كلمة المرور</h3>
                  <p>لقد طلبت إعادة تعيين كلمة المرور لحسابك في FixIt.</p>
                  <a href='{resetLink}'>اضغط هنا لتعيين باسورد جديد</a>";

            await _emailService.SendEmailAsync(user.Email, "إعادة تعيين كلمة المرور", body);

            return Ok(new { message = "تم إرسال رابط إعادة تعيين كلمة المرور إلى بريدك الإلكتروني." });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            var user = _UserService.Find(u => u.Email == dto.Email && u.ConfirmationToken == dto.Token).FirstOrDefault();

            if (user == null) return BadRequest("الرابط غير صالح أو انتهت صلاحيته.");

            // تغيير الباسورد وتشفيره
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.ConfirmationToken = null; // مسح التوكن لزيادة الأمان

            await _UserService.UpdateAsync(user);

            return Ok(new { message = "تم تغيير كلمة المرور بنجاح. يمكنك الآن تسجيل الدخول." });
        }
    }
}
