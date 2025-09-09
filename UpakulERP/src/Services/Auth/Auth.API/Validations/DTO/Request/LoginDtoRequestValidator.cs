using Auth.API.DTO.Request;
using FluentValidation;

namespace Auth.API.Validations.DTO.Request
{
    public class LoginDtoRequestValidator : AbstractValidator<LoginDtoRequest>
    {
        public LoginDtoRequestValidator()
        {
            RuleFor(x => x.UserId)
                        .NotEmpty()
                        .NotNull()
                        .WithMessage("User id is required.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Password is required.");
        }
    }
}
