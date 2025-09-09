using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Features.DBOrders.Commands.Create.Validations
{
  public  class CreateLeaveSetupValidator : AbstractValidator<CreateLeaveSetupCommand>
    {
        public CreateLeaveSetupValidator()
        {
            RuleFor(x => x.LeaveCategoryId)
              .NotEmpty()
              .WithMessage("LeaveCategoryId is required.");
            RuleFor(x => x.LeaveTypeName)
             .NotEmpty()
             .WithMessage("LeaveType Name is required.");
            RuleFor(x => x.YearlyMaxLeave)
             .NotEmpty()
             .WithMessage("Yearly Max Leave is required.");
            RuleFor(x => x.YearlyMaxAvailDays)
            .NotEmpty()
            .WithMessage("Yearly Max Avail Days is required.");
            RuleFor(x => x.EffectiveStartDate)
             .NotEmpty()
             .WithMessage("Effective Start Date is required.");
        }
    }
}

