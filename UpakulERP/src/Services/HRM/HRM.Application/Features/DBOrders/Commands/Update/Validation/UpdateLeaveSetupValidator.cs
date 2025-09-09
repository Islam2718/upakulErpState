using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Features.DBOrders.Commands.Update.Validation
{
  public  class UpdateLeaveSetupValidator : AbstractValidator<UpdateLeaveSetupCommand>
    {
        public UpdateLeaveSetupValidator()
        {
            RuleFor(x => x.LeaveCategoryId)
              .NotEmpty()
              .WithMessage("Leave Category is required.");
            RuleFor(x => x.LeaveTypeName)
             .NotEmpty()
             .WithMessage("LeaveType Name is required.");
            RuleFor(x => x.EmployeeTypeId)
             .NotEmpty()
             .WithMessage("Employee Type is required.");
           
        }
    }
}