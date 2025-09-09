using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Features.DBOrders.Commands.Create.Validations
{
   public class CreateEducationValidator : AbstractValidator<CreateEducationCommand>
    {
        public CreateEducationValidator()
        {
            RuleFor(x => x.EducationName)
              .NotEmpty()
              .WithMessage("Education name is required.");

        }
    }
}
