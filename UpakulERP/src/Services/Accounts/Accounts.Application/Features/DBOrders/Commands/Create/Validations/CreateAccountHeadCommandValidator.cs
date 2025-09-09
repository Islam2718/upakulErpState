using Accounts.Application.Features.DBOrders.Commands.Create.Commands;
using FluentValidation;
using Utility.Constants;

namespace Accounts.Application.Features.DBOrders.Commands.Create.Validations
{
   public class CreateAccountHeadCommandValidator : AbstractValidator<CreateAccountHeadCommand>
    {
        public CreateAccountHeadCommandValidator()
        {
            RuleFor(x => x.HeadName)
              .NotEmpty()
              .WithMessage("Head name is required.")
               .Length(1, 250).WithMessage(MessageTexts.string_length("Marital status", 250)); ;

        }
    }
}
