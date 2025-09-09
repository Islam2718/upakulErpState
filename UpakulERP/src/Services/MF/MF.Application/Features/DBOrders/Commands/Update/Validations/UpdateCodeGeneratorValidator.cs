using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Application.Features.DBOrders.Commands.Update.Validations
{
   public class UpdateCodeGeneratorValidator : AbstractValidator<UpdateCodeGeneratorCommand>
    {
        public UpdateCodeGeneratorValidator()
        {
            RuleFor(x => x.TypeName)
             .NotEmpty()
              .WithMessage("Type Name is required.");

            RuleFor(x => x.Description)
             .NotEmpty()
              .WithMessage("Description is required.");

            //RuleFor(x => x.CodeLength)
            // .NotEmpty()
            //  .WithMessage("CodeLength is required.");

            RuleFor(x => x.MainJoinCode)
             .NotEmpty()
             .WithMessage("MainJoinCode is required.");

            RuleFor(x => x.VirtualJoinCode)
              .NotEmpty()
              .WithMessage("VirtualJoinCode is required.");

        }
    }
}

