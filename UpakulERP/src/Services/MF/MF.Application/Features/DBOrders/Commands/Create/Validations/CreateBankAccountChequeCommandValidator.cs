using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using Utility.Constants;

namespace MF.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateBankAccountChequeCommandValidator : AbstractValidator<CreateBankAccountChequeCommand>
    {
        public CreateBankAccountChequeCommandValidator()
        {
            RuleFor(x => x.BankAccountMappingId)
             .NotEmpty()
              .WithMessage("Bank account mapping is required.");

            RuleFor(x => x.ChequeNumberPrefix)
             .NotEmpty().WithMessage("Cheque prefix is required.")
             .Length(1, 5).WithMessage(MessageTexts.string_length("Prefix", 5));

            RuleFor(x => x.ChequeNumberFrom)
             .NotEmpty().WithMessage("Cheque number(from) is required.")
             .Length(1, 15).WithMessage(MessageTexts.string_length("Prefix", 15)); 

            RuleFor(x => x.ChequeNumberTo)
             .NotEmpty().WithMessage("Cheque number(to) is required.")
             .Length(1, 15).WithMessage(MessageTexts.string_length("Prefix", 15));

        }
    }
}
