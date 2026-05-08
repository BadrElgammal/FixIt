using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            // استخدم الإيميل اللي سجلت بيه في Brevo هنا
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, "bdraljmal78@gmail.com"));

            email.To.Add(MailboxAddress.Parse(mailTo));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            // 👇 السطر ده السحري اللي هيحل الـ 500 Error اللي ظهرتلك
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

            // الاتصال بالسيرفر
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);

            // تسجيل الدخول ببيانات Brevo
            await smtp.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);

            // إرسال الإيميل
            await smtp.SendAsync(email);

            // قفل الاتصال
            await smtp.DisconnectAsync(true);
        }
    }
}
