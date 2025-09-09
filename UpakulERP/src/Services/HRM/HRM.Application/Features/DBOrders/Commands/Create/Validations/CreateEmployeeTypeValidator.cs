using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;

namespace HRM.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateEmployeeTypeValidator : AbstractValidator<CreateEmployeeTypeCommand>
    {
        public CreateEmployeeTypeValidator()
        {
            RuleFor(x => x.EmployeeTypeName)
              .NotEmpty()
              .WithMessage("Employee Type is required.");

        }
    }
}
