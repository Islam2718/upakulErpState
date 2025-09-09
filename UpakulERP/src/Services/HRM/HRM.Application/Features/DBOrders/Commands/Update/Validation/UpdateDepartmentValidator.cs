using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;

namespace HRM.Application.Features.DBOrders.Commands.Update.Validation
{
    public class UpdateDepartmentValidator: AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
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
