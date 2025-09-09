using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using Utility.Constants;

namespace MF.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateComponentValidator : AbstractValidator<CreateComponentCommand>
    {
        public CreateComponentValidator()
        {
            RuleFor(x => x.ComponentCode)
             .NotEmpty()
              .WithMessage("Component Code is required.");

            RuleFor(x => x.ComponentName)
             .NotEmpty()
              .WithMessage("Component Name is required.");

            RuleFor(x => x.ComponentType)
             .NotEmpty()
              .WithMessage("Component type is required.")
               .Length(1, 1).WithMessage(MessageTexts.string_length("Component type", 1));

            RuleFor(x => x.LoanType)
                .Must((obj, loantype) => !(loantype == null && obj.ComponentType == "L"))
                .WithMessage(MessageTexts.required_field("Loan type"))
                .Must((obj, loantype) => !(loantype == "P" && obj.SavingMap == true))
                .WithMessage("Do not allow checking to save maps for projects.")
                .Length(1, 1).WithMessage(MessageTexts.string_length("loan type", 1));
            RuleFor(x => x.InterestRate)
            .NotEmpty()
            .WithMessage(MessageTexts.required_field("Rate"));

        }
    }
}