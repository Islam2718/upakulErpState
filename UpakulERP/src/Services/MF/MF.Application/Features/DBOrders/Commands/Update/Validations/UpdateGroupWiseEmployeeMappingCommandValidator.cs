using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Update.Command;

namespace MF.Application.Features.DBOrders.Commands.Update.Validations
{

    public class UpdateGroupWiseEmployeeMappingCommandValidator : AbstractValidator<UpdateGroupWiseEmployeeAssignCommand>
    {
        public UpdateGroupWiseEmployeeMappingCommandValidator()
        {
            RuleFor(x => x.Id)
             .NotEmpty()
              .WithMessage("Id is required.");
        }
    }

}
