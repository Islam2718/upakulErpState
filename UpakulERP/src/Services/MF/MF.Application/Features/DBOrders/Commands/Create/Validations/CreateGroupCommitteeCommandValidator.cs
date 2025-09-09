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
    public class CreateGroupCommitteeCommandValidator : AbstractValidator<CreateGroupCommitteeCommand>
    {
        public CreateGroupCommitteeCommandValidator()
        {
            //RuleFor(x => x.GroupId)
            //  .NotEmpty().WithMessage("Group is required.");

            //RuleFor(x => x.MemberId)
            //   .NotEmpty().WithMessage("Member is required.");

            //RuleFor(x => Convert.ToDateTime(x.StartDate))
            //    .NotEmpty().WithMessage("Start Date is required.")
            //    .LessThanOrEqualTo(DateTime.Today).WithMessage("Start Date cannot be in the future.");
            
            //RuleFor(x => x.CommitteePositionId)
            //    .NotEmpty().WithMessage("Committee Possition is required.");
        }
    }


}
