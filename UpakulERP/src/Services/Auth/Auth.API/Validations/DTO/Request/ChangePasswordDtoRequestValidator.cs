using Auth.API.DTO.Request;
using FluentValidation;

namespace Auth.API.Validations.DTO.Request
{
    public class ChangePasswordDtoRequestValidator : AbstractValidator<ChangePasswordDtoRequest>
    {
        public ChangePasswordDtoRequestValidator()
        {
            //RuleFor(x => x.UserName)
            //   .NotEmpty()
            //   .WithMessage("User name is required.");

            RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithMessage("Current password is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("New password is required.")
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!])(?=\S+$).{5,15}$")
                .WithMessage(
                    "Password must be between 5 and 15 characters, and include at least one digit, one lowercase letter, one uppercase letter, and one special character.")
                .NotEqual(x => x.CurrentPassword)
                .WithMessage("New password must be different from old password.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm password is required.")
                .Equal(x => x.NewPassword)
                .WithMessage("New and confirm password must be equal.");
        }
    }
}
