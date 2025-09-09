using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Create.Command;

namespace HRM.Application.Features.DBOrders.Commands.Create.Validations
{
    public class CreateDepartmentValidations : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidations()
        {
            RuleFor(x => x.DepartmentName)
              .NotEmpty()
              .WithMessage("Department Name is required.");
            RuleFor(x => x.OrderNo)
              .NotEmpty()
              .WithMessage("Order No is required.");
        }
    }
}
