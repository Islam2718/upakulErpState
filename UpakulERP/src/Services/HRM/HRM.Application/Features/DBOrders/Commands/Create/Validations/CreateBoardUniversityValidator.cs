using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateBoardUniversityValidator : AbstractValidator<CreateBoardUniversityCommand>
    {
        public CreateBoardUniversityValidator()
        {
            RuleFor(x => x.BUName)
              .NotEmpty()
              .WithMessage("Board/University is required.");

        }
    }
}
