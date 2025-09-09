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
    public class UpdateGroupWiseEmployeeAssignCommandValidator : AbstractValidator<UpdateGroupWiseEmployeeAssignCommand>
    {
        public UpdateGroupWiseEmployeeAssignCommandValidator()
        {
            RuleFor(x => x.Id)
             .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.ReleaseNote)
             .NotEmpty().WithMessage("Release note is required.")
             .Length(1, 200).WithMessage(MessageTexts.string_length("Release note name", 200));

        }
    }


}
