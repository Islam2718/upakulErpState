using FluentValidation;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;

namespace HRM.Application.Features.DBOrders.Commands.Update.Validation
{
    public class UpdateBoardUniversityValidator: AbstractValidator<UpdateBoardUniversityCommand>
    {
        public UpdateBoardUniversityValidator()
        {
            RuleFor(x => x.BUName)
              .NotEmpty()
              .WithMessage("Board/University is required.");

        }
    }
}
