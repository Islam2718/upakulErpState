using Auth.API.DTO.Request;
using FluentValidation;

namespace Auth.API.Validations.DTO.Request
{
    public class UserDtoRequestValidator : AbstractValidator<UserDtoRequest>
    {
        public UserDtoRequestValidator()
        {
            
            
            RuleFor(x => x.EmployeeId)
              .GreaterThan(x => x.EmployeeId)
              .WithMessage("Employee is required.");

            RuleFor(x => x.UserName)
               .NotEmpty()
               .WithMessage("User name is required.");


            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!])(?=\S+$).{5,15}$")
                .WithMessage(
                    "Password must be between 5 and 15 characters, and include at least one digit, one lowercase letter, one uppercase letter, and one special character.");
                

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm password is required.")
                .Equal(x => x.Password)
                .WithMessage("New and confirm password must be equal.");
        }
    }
}
