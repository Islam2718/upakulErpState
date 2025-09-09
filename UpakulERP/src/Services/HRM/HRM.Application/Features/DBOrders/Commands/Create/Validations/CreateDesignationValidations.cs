using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;

namespace HRM.Application.Features.DBOrders.Commands.Create.Validations
{
   public class CreateDesignationValidations : AbstractValidator<CreateDesignationCommand>
   {
        public CreateDesignationValidations()
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
 
