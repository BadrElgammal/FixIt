using FixIt.Core.Features.Clients.Commands.Models;
using FixIt.Service.Abstracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Clients.Commands.Validatiors
{
    public class ChangeClientPasswordValidator : AbstractValidator<ChangeClientPasswordCommand>
    {
        private readonly IClientService _clientService;

        public ChangeClientPasswordValidator(IClientService clientService)
        {
            _clientService = clientService;
            ApplayValidationsRuls();
            ApplayCustomValidationRuls();
        }

        public void ApplayValidationsRuls()
        {

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("كلمة المرور الحاليه مطلوبه")
                .NotNull().WithMessage("كلمة المرور الحاليه مطلوبه")
                .MinimumLength(6).WithMessage("كلمة المرور صغيره جدا");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("كلمة المرور الجديده مطلوبه")
                .NotNull().WithMessage("كلمة المرور الجديده مطلوبه")
                .MinimumLength(6).WithMessage("كلمة المرور صغيره جدا");

            RuleFor(x => x.ConfarmPassword)
                .Equal(x => x.NewPassword).WithMessage("كلمة المرور وتاكيدها يجب ان يكونا متطابقان")
                .MinimumLength(6).WithMessage("كلمة المرور صغيره جدا");
        }

        public void ApplayCustomValidationRuls()
        {

        }
    }
}
