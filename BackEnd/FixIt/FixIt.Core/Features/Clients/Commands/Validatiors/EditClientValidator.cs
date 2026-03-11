using FixIt.Core.Features.Clients.Commands.Models;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Clients.Commands.Validatiors
{
    public class EditClientValidator : AbstractValidator<EditClientCommand>
    {
        private readonly IClientService _clientService;

        public EditClientValidator(IClientService clientService)
        {
            _clientService = clientService;
            ApplayValidationsRuls();
            ApplayCustomValidationRuls();
        }

        public void ApplayValidationsRuls()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("الاسم مطلوب")
                .NotNull().WithMessage("الاسم مطلوب")
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(x => x.Phone)
                .NotNull().WithMessage("رقم الهاتف مطلوب")
                .Matches(@"^(010|011|012|015)[0-9]{8}$").WithMessage("رقم الهاتف يجب ان يكون على الصيغه '01xxxxxxxxx'");

            RuleFor(x => x.City)
                .NotNull().WithMessage("اسم المدينه مطلوب");
        }

        public void ApplayCustomValidationRuls()
        {

        }
    }
}
