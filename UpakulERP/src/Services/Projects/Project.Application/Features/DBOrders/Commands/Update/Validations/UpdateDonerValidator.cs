using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Project.Application.Features.DBOrders.Commands.Update.Commands;
using Utility.Constants;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Commands.Update.Validations
{
    public class UpdateDonerValidator : AbstractValidator<UpdateDonerCommand>
    {
        public UpdateDonerValidator()
        {
            RuleFor(x => x.CountryId)
             .NotEmpty().WithMessage("Country is required.");
            RuleFor(x => x.DonerName)
             .NotEmpty().WithMessage("Doner name is required.")
             .Length(1, 50).WithMessage(MessageTexts.string_length("Doner name", 50));

            RuleFor(x => x.DonerCode)
            .NotEmpty().WithMessage("Doner code is required.")
            .Length(1, 10).WithMessage(MessageTexts.string_length("Doner code", 15));

            RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .Length(1, 150).WithMessage(MessageTexts.string_length("Location", 150));

        }
    }
}
