using System.Threading.Tasks;
using FixIt.Service.Abstracts;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using FixIt.Core.Settings;

namespace FixIt.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string mailTo, string subject, string body)
        {
            var email = new MimeMessage();

            // 💡 التعديل الأهم: استخدمنا الإيميل من الإعدادات بدلاً من كتابته يدوياً في الكود
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName ?? "FixIt Support", _mailSettings.Email));

            email.To.Add(MailboxAddress.Parse(mailTo));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            // السطر ده مفيد جداً في بيئة التطوير لتجنب مشاكل شهادات الـ SSL
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

            // الاتصال بسيرفر Gmail باستخدام إعدادات appsettings.json
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);

            // تسجيل الدخول بالإيميل الجديد والـ App Password المكون من 16 حرف
            await smtp.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);

            // إرسال الإيميل
            await smtp.SendAsync(email);

            // قفل الاتصال
            await smtp.DisconnectAsync(true);
        }
    }
}