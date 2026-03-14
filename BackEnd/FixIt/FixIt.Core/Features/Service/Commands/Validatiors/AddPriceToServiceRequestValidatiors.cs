using FixIt.Core.Features.Service.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Commands.Validatiors
{
    public class AddPriceToServiceRequestValidatiors : AbstractValidator<AddPriceToServiceRequestCommand>
    {


        public AddPriceToServiceRequestValidatiors()
        {
            ApplayValidationsRuls();
            ApplayCustomValidationRuls();
        }

        public void ApplayValidationsRuls()
        {

            RuleFor(x => x.WorkerId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.RequestId)
               .NotEmpty()
               .NotNull();

            RuleFor(x => x.TotalPrice)
                .NotEmpty().WithMessage("السعر مطلوب")
                .NotNull().WithMessage("السعر مطلوب");

        }

        public void ApplayCustomValidationRuls()
        {

        }
    }
}
