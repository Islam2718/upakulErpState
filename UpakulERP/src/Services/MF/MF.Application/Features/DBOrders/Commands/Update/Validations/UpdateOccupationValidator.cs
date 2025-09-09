using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Update.Command;

namespace MF.Application.Features.DBOrders.Commands.Update.Validations
{
    public class UpdateOccupationValidator : AbstractValidator<UpdateOccupationCommand>
    {
        public UpdateOccupationValidator()
        {
            RuleFor(x => x.OccupationName)
                 .NotEmpty()
                  .WithMessage("Occupation is required.");
        }
    }


}
