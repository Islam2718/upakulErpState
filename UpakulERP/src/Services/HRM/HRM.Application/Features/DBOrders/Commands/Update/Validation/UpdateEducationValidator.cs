using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;

namespace HRM.Application.Features.DBOrders.Commands.Update.Validation
{
    public class UpdateEducationValidator : AbstractValidator<UpdateEducationCommand>
    {
        public UpdateEducationValidator()
        {
            RuleFor(x => x.EducationName)
              .NotEmpty()
              .WithMessage("Education name is required.");

        }
    }
}
