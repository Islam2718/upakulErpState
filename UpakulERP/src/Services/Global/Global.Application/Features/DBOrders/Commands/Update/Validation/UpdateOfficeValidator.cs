using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Global.Application.Features.DBOrders.Commands.Create.Commands;
using Global.Application.Features.DBOrders.Commands.Update.Command;

namespace Global.Application.Features.DBOrders.Commands.Update.Validation
{
    public class UpdateOfficeValidator: AbstractValidator<UpdateOfficeCommand>
    {
        public UpdateOfficeValidator()
        {
            RuleFor(x => x.OfficeCode)
              .NotEmpty()
              .WithMessage("Code is required.");

            RuleFor(x => x.OfficeName)
              .NotEmpty()
              .WithMessage("Name is required.");

            RuleFor(x => x.OfficeType)
             .NotEmpty()
             .WithMessage("Type is required.");
        }
    }
}
