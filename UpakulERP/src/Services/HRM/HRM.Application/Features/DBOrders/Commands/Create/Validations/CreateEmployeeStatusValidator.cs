using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;

namespace HRM.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateEmployeeStatusValidator : AbstractValidator<CreateEmployeeStatusCommand>
    {
        public CreateEmployeeStatusValidator()
        {
            RuleFor(x => x.EmployeeStatusName)
              .NotEmpty()
              .WithMessage("Employee Status is required.");

        }
    }
}
