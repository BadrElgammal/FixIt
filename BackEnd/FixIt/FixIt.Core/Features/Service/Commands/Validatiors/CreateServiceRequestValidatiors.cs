using FixIt.Core.Features.Clients.Commands.Models;
using FixIt.Core.Features.Service.Commands.Models;
using FixIt.Service.Abstracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Commands.Validatiors
{
    public class CreateServiceRequestValidatiors : AbstractValidator<CreateServiceRequestCommand>
    {


        public CreateServiceRequestValidatiors()
        {
            ApplayValidationsRuls();
            ApplayCustomValidationRuls();
        }

        public void ApplayValidationsRuls()
        {

            RuleFor(x => x.WorkerId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ClientId)
               .NotEmpty()
               .NotNull();

            RuleFor(x => x.ServiceTitle)
                .NotEmpty().WithMessage("العنوان مطلوب")
                .NotNull().WithMessage("العنوان مطلوب")
                .MinimumLength(6).WithMessage("العنوان يجب ان يزيد عن 6 احرف");

            RuleFor(x => x.ServiceDescription)
               .NotEmpty().WithMessage("الوصف مطلوب")
               .NotNull().WithMessage("الوصف مطلوب")
               .MinimumLength(30).WithMessage("الوصف يجب ان يزيد عن 30 احرف");
        }

        public void ApplayCustomValidationRuls()
        {

        }
    }
}
