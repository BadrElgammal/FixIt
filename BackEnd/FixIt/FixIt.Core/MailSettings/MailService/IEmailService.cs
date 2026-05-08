using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Service.Abstracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(string mailTo, string subject, string body);
    }
}
