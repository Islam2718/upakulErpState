using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounts.Application.Features.DBOrders.Commands.Create.Commands;
using FluentValidation;

namespace Accounts.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateBudgetEntryValidator : AbstractValidator<CreateBudgetEntryCommand>
    {
        public CreateBudgetEntryValidator()
        {
            RuleFor(x => x.FinancialYear)
              .NotEmpty()
              .WithMessage("Financial Year is required.");

            RuleFor(x => x.OfficeId)
              .NotEmpty()
              .WithMessage("Office is required.");

            RuleFor(x => x.ComponentId)
              .NotEmpty()
              .WithMessage("Component is required.");

            RuleFor(x => x.ComponentParentId)
              .NotEmpty()
              .WithMessage("Component Head is required.");

        }
    }
}
