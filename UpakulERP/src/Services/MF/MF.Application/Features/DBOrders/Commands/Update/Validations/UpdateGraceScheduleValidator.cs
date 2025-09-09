using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Update.Command;

namespace MF.Application.Features.DBOrders.Commands.Update.Validations
{
    public class UpdateGraceScheduleValidator : AbstractValidator<UpdateGraceScheduleCommand>
    {
        public UpdateGraceScheduleValidator()
        {
            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithMessage("Reason is required.");

            RuleFor(x => x.OfficeId)
                .GreaterThan(0)
                .WithMessage("office is required.");

            RuleFor(x => x.GraceFrom)
                .NotEmpty()
                .Must(d => DateTime.TryParse(d, out _))
                .WithMessage("From date is required and must be valid.");

            RuleFor(x => x.GraceTo)
                .NotEmpty()
                .Must((x, d) =>
                    DateTime.TryParse(d, out var toDate) &&
                    DateTime.TryParse(x.GraceFrom, out var fromDate) &&
                    toDate > fromDate
                )
                .WithMessage("To date must be greater than From date.");


        }
    }
}
