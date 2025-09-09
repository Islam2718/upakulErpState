using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Global.Application.Features.DBOrders.Commands.Update.Command;
using Utility.Constants;

namespace Global.Application.Features.DBOrders.Commands.Update.Validation
{
    public class UpdateCountryValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryValidator()
        {
            RuleFor(x => x.CountryName)
             .NotEmpty().WithMessage("Country name is required.")
             .Length(1, 50).WithMessage(MessageTexts.string_length("Doner name", 50));

            RuleFor(x => x.CountryCode)
            .NotEmpty().WithMessage("Doner code is required.")
            .Length(1, 10).WithMessage(MessageTexts.string_length("Doner code", 15));

        }
    }
}
