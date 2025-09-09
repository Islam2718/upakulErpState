using Auth.API.DTO.Request;
using FluentValidation;

namespace Auth.API.Validations.DTO.Request
{
    public class ResetPasswordDtoRequestValidator : AbstractValidator<ResetPasswordDtoRequest>
    {
        public ResetPasswordDtoRequestValidator()
        {
            RuleFor(x => x.UserName)
                   .NotEmpty()
                   .WithMessage("User name is required.");
            RuleFor(x => x.NewPassword)
               .NotEmpty()
               .WithMessage("New password is required.")
               .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!])(?=\S+$).{5,15}$")
               .WithMessage(
                   "Password must be between 5 and 15 characters, and include at least one digit, one lowercase letter, one uppercase letter, and one special character.");
        }
    }
}
