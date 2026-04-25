using FixIt.Core.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FixIt.Core.Features.Admin.Command.Models
{
    public class ChangeAdminPasswordCommand : IRequest<Response<string>>
    {
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "كلمة المرور الحالية مطلوبة")]
        [MinLength(6, ErrorMessage = "كلمة المرور صغيرة جداً")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "كلمة المرور الجديدة مطلوبة")]
        [MinLength(6, ErrorMessage = "كلمة المرور صغيرة جداً")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
        [MinLength(6, ErrorMessage = "كلمة المرور صغيرة جداً")]
        [Compare("NewPassword", ErrorMessage = "كلمة المرور وتأكيدها يجب أن يكونا متطابقان")]
        public string ConfarmPassword { get; set; }
    }
}
