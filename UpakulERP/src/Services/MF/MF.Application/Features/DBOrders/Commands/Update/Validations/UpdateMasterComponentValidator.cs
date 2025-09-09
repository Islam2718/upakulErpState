using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;

namespace MF.Application.Features.DBOrders.Commands.Update.Validations
{
  public  class UpdateMasterComponentValidator : AbstractValidator<UpdateMasterComponentCommand>
    {
        public UpdateMasterComponentValidator()
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
