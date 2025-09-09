using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using Utility.Constants;

namespace MF.Application.Features.DBOrders.Commands.Update.Validations
{
    public class UpdateBankAccountMappingCommandValidator : AbstractValidator<UpdateBankAccountMappingCommand>
    {
        public UpdateBankAccountMappingCommandValidator()
        {
            RuleFor(x => x.BankId)
             .NotEmpty().WithMessage("Bank is required.");

            RuleFor(x => x.OfficeId)
             .NotEmpty().WithMessage("Office is required.");

            //RuleFor(x => x.BankBranchId)
            //    .NotEmpty().WithMessage("Branch is required.");

            RuleFor(x => x.BankAccountNumber)
              .NotEmpty().WithMessage("Account number is required.")
               .Length(1, 100).WithMessage(MessageTexts.string_length("Group member name", 100));
        }
    }


}
