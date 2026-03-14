using FixIt.Core.Features.Workers.Commands.Models;
using FixIt.Service.Abstracts;
using FluentValidation;

namespace FixIt.Core.Features.Workers.Commands.Validatiors
{
    public class EditeWorkerValidator : AbstractValidator<EditeWorkerCommand>
    {

        //   UserId 
        //   FullName
        //   Email 
        //   Phone 
        //   City 
        //    ImgUrl
        //   Role 
        //   IsActive 
        //   LastLogin 
        //   CreatedAt 
        //   UpdatedAt 
        //   JobTitle 
        //   Description 
        //   AvailabilityStatus 
        //   RatingAverage 
        //   Area 
        //   CategoryName  



        private readonly IWorkerService _workerService;

        public EditeWorkerValidator(IWorkerService workerService)
        {
            _workerService = workerService;
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
