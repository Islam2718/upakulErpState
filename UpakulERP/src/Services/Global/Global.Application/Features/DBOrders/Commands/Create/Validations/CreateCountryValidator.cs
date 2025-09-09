using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Global.Application.Features.DBOrders.Commands.Create.Commands;
using Utility.Constants;

namespace Global.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateCountryValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryValidator()
        {
            RuleFor(x => x.CountryName)
             .NotEmpty().WithMessage("Name is required.")
             .Length(1, 50).WithMessage(MessageTexts.string_length("Country Name", 50)); ;

            RuleFor(x => x.CountryCode)
              .NotEmpty().WithMessage("Code is required.")
              .Length(1, 10).WithMessage(MessageTexts.string_length("Country Code", 10));


        }
    }
}
