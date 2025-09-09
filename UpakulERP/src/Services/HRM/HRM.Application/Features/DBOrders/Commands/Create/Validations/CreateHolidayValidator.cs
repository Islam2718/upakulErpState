using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateHoliDayValidator : AbstractValidator<CreateHoliDayCommand>
    {
        public CreateHoliDayValidator()
        {
            RuleFor(x => x.HoliDayName)
              .NotEmpty()
              .WithMessage("HoliDay Name is required.");

            RuleFor(x => x.StartDate)
           .NotEmpty()
           .WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("End date is required.")
            .GreaterThanOrEqualTo(r => r.StartDate)
            .WithMessage("End date should be greater than or equal to start date");
        }
    }
}
