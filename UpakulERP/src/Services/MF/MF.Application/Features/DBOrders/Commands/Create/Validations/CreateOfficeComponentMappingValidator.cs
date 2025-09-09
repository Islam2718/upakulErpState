using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Create.Commands;

namespace MF.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateOfficeComponentMappingValidator : AbstractValidator<CreateOfficeComponentMappingCommand>
    {
        public CreateOfficeComponentMappingValidator()
        {
            RuleFor(x => x.ComponentId)
              .NotEmpty()
              .WithMessage("Component is required.");

        }
    }
}
