using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateGraceScheduleValidator : AbstractValidator<CreateGraceScheduleCommand>

    {
        public CreateGraceScheduleValidator()
        {
            RuleFor(x => x.Reason)
               .NotEmpty()
               .WithMessage("Reason is required.");

            RuleFor(x => x.OfficeId)
                .NotNull()
                .WithMessage("Office  is required.");

            //RuleFor(x => x.GroupId)
            //    .NotNull()
            //    .WithMessage("Group Id is required.");

            // LoanId is optional, no rule added.

            RuleFor(x => x.GraceFrom)
                .NotEmpty()
                .WithMessage("From date is required.");

            RuleFor(x => x.GraceTo)
                .NotEmpty()
                .WithMessage("To date is required.")
                 .GreaterThan(r => r.GraceFrom)
             .WithMessage("From date should be greter then to date");


        }
    }
}
