using FluentValidation;
using Global.Application.Features.DBOrders.Commands.Create.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;

namespace Global.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateBankValidator : AbstractValidator<CreateBankCommand>
    {
        public CreateBankValidator()
        {
            RuleFor(x => x.BankType)
                .NotEmpty().WithMessage("Bank type is required.")
                .Length(1, 1).WithMessage("Bank type is single character is required.");
            RuleFor(x => x.BankName)
                .NotEmpty().WithMessage("Bank name is required.")
                .Length(1, 100).WithMessage(MessageTexts.string_length("Bank name", 100));
            RuleFor(x => x.BankShortCode)
                .NotEmpty().WithMessage("Bank Short Code is required.")
                .Length(1, 10).WithMessage(MessageTexts.string_length("Bank short code", 10));
        }
    }
}
