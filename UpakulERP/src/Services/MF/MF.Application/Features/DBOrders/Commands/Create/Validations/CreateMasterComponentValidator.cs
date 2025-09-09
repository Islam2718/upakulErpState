using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;

namespace MF.Application.Features.DBOrders.Commands.Create.Validations
{
   public class CreateMasterComponentValidator : AbstractValidator<CreateMasterComponentCommand>
    {
        public CreateMasterComponentValidator()
        {
            RuleFor(x => x.Name)
             .NotEmpty()
              .WithMessage("Name is required.");

            RuleFor(x => x.Code)
             .NotEmpty()
              .WithMessage("Code is required.");


        }
    }
}