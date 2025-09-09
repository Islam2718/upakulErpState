using FluentValidation;
using Global.Application.Features.DBOrders.Commands.Update.Command;

namespace Global.Application.Features.DBOrders.Commands.Create.Validations
{
    public class UpdateBankValidator : AbstractValidator<UpdateBankCommand>
    {
        public UpdateBankValidator()
        {
            RuleFor(x => x.BankType)
              .NotEmpty().WithMessage("Bank Type is required.")
              .Length(1, 1).WithMessage("Bank type is single character is required.");
            RuleFor(x => x.BankName)
                .NotEmpty().WithMessage("Bank name is required.")
                .Length(1, 100).WithMessage("Bank name length max 100");
            RuleFor(x => x.BankShortCode)
                .NotEmpty().WithMessage("Bank short code is required.")
                .Length(1, 10).WithMessage("Bank short code length max 100");

        }
    }
}
