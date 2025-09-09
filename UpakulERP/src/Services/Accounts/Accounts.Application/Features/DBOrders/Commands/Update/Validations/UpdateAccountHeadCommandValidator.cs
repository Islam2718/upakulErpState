using Accounts.Application.Features.DBOrders.Commands.Update.Commands;
using FluentValidation;
using Utility.Constants;

namespace Accounts.Application.Features.DBOrders.Commands.Update.Validations
{
   public class UpdateAccountHeadCommandValidator : AbstractValidator<UpdateAccountHeadCommand>
    {
        public UpdateAccountHeadCommandValidator()
        {
            RuleFor(x => x.AccountId)
              .NotEmpty()
              .WithMessage("Id is required.");
            RuleFor(x => x.HeadName)
              .NotEmpty()
              .WithMessage("Head name is required.")
               .Length(1, 250).WithMessage(MessageTexts.string_length("Marital status", 250)); ;

        }
    }
}
