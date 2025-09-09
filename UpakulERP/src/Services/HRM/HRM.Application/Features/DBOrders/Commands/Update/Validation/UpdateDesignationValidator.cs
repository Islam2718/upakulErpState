using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;

namespace HRM.Application.Features.DBOrders.Commands.Update.Validation
{
    public class UpdateDesignationValidator : AbstractValidator<UpdateDesignationCommand>
    {
        public UpdateDesignationValidator()
        {
            RuleFor(x => x.DesignationName)
              .NotEmpty()
              .WithMessage("Designation Name is required.");
            RuleFor(x => x.OrderNo)
              .NotEmpty()
              .WithMessage("OrderNo is required.");
        }
    }
}
