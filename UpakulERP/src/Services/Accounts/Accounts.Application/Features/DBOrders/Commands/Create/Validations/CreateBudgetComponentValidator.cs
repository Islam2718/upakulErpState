using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounts.Application.Features.DBOrders.Commands.Create.Commands;
using FluentValidation;

namespace Accounts.Application.Features.DBOrders.Commands.Create.Validations
{
   public class CreateBudgetComponentValidator : AbstractValidator<CreateBudgetComponentCommand>
    {
        public CreateBudgetComponentValidator()
        {
            RuleFor(x => x.ComponentName)
              .NotEmpty()
              .WithMessage("Component name is required.");

        }
    }
}
