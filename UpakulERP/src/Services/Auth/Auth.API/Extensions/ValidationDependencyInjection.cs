using Auth.API.DTO.Request;
using Auth.API.Validations.DTO.Request;
using FluentValidation;

namespace Auth.API.Extensions
{
    public static class ValidationDependencyInjection
    {
        public static void AddValidationDependencyInjection(this IServiceCollection service)
        {
            service.AddValidatorsFromAssemblyContaining<ChangePasswordDtoRequestValidator>();
            service.AddScoped<IValidator<ChangePasswordDtoRequest>, ChangePasswordDtoRequestValidator>();
            
            service.AddValidatorsFromAssemblyContaining<ResetPasswordDtoRequestValidator>();
            service.AddScoped<IValidator<ResetPasswordDtoRequest>, ResetPasswordDtoRequestValidator>();

            service.AddValidatorsFromAssemblyContaining<LoginDtoRequestValidator>();
            service.AddScoped<IValidator<LoginDtoRequest>, LoginDtoRequestValidator>();
        }
    }
}
