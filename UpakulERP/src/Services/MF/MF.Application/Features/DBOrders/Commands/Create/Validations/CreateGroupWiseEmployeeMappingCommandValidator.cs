using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Create.Commands;

namespace MF.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateGroupWiseEmployeeMappingCommandValidator : AbstractValidator<CreateGroupWiseEmployeeAssignCommand>
    {
        public CreateGroupWiseEmployeeMappingCommandValidator()
        {
            //Write Rule based on requirements
            //RuleFor(x => x.AssignedGroupListId)
            //  .NotEmpty()
            //  .WithMessage("Assigned Group is required.");


        }
    }
}
