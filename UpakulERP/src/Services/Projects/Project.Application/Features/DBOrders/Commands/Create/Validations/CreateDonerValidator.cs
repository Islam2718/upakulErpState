using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Project.Application.Features.DBOrders.Commands.Create.Commands;
using Utility.Constants;

namespace Project.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateDonerValidator : AbstractValidator<CreateDonerCommand>
    {
        public CreateDonerValidator()
        {
            RuleFor(x => x.DonerName)
             .NotEmpty().WithMessage("Name is required.")
             .Length(1, 50).WithMessage(MessageTexts.string_length("Doner Name", 50)); ;

            RuleFor(x => x.DonerCode)
              .NotEmpty().WithMessage("Code is required.")
              .Length(1, 10).WithMessage(MessageTexts.string_length("Doner Code", 10));
        }
    }
}
