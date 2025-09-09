using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Create.Commands;

namespace MF.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateBankAccountMappingCommandValidator : AbstractValidator<CreateBankAccountMappingCommand>
    {
        public CreateBankAccountMappingCommandValidator()
        {
            RuleFor(x => x.BankId)
             .NotEmpty()
              .WithMessage("Bank is required.");

            //RuleFor(x => x.BankBranchId)
            // .NotEmpty()
            //  .WithMessage("Branch is required.");

            RuleFor(x => x.BankAccountNumber)
             .NotEmpty()
              .WithMessage("Account number is required.");

        }
    }
}
