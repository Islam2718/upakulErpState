using FluentValidation;
using MF.Application.Features.DBOrders.Commands.Create.Commands;

namespace MF.Application.Features.DBOrders.Commands.Create.Validations
{
   public class CreateCodeGeneratorValidator : AbstractValidator<CreateIdGenerateCommand>
    {
        public CreateCodeGeneratorValidator()
        {
            RuleFor(x => x.TypeName)
             .NotEmpty()
              .WithMessage("Type Name is required.");

            RuleFor(x => x.Description)
             .NotEmpty()
              .WithMessage("Description is required.");

            RuleFor(x => x.CodeLength)
             .NotEmpty()
              .WithMessage("CodeLength is required.");

            RuleFor(x => x.MainJoinCode)
             .NotEmpty()
             .WithMessage("MainJoinCode is required.");

           RuleFor(x => x.VirtualJoinCode)
             .NotEmpty()
             .WithMessage("VirtualJoinCode is required.");


        }
    }
}