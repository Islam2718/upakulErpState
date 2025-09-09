using Auth.API.DTO.Request;
using FluentValidation;

namespace Auth.API.Validations.DTO.Request
{
    public class RoleDtoRequestValidator : AbstractValidator<CreateRoleDtoRequest>
    {
        public RoleDtoRequestValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("Role name is required.");

            
        }
    }
}
