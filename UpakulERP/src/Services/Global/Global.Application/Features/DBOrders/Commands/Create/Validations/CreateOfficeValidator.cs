using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Global.Application.Features.DBOrders.Commands.Create.Command;
using Global.Application.Features.DBOrders.Commands.Create.Commands;

namespace Global.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateOfficeValidator : AbstractValidator<CreateOfficeCommand>
    {
        public CreateOfficeValidator()
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
