using Accounts.Application.Features.DBOrders.Commands.Update.Command;
using FluentValidation;
using Utility.Constants;

namespace Accounts.Application.Features.DBOrders.Commands.Update.Validations
{
    public class UpdateBudgetComponentValidator : AbstractValidator<UpdateBudgetComponentCommand>
    {
        public UpdateBudgetComponentValidator()
        {
            RuleFor(x => x.Id)
             .NotEmpty().WithMessage("Budget Component id is required.");
            RuleFor(x => x.ComponentName)
             .NotEmpty().WithMessage("Budget Component name is required.")
             .Length(1, 25).WithMessage(MessageTexts.string_length("Budget Component", 25));


        }
    }
}
