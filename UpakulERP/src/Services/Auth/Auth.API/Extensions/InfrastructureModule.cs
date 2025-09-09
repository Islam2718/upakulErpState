using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Auth.API.Extensions
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection service)
        {
            //service.AddValidationDependencyInjection();
            //service.AddMappingProfileDependencyInjection();
            //service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddMappingProfileDependencyInjection();
            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            service.AddFluentValidationAutoValidation();
            service.AddDependencyInjectionRepositories();
            service.AddDependencyInjectionServices();
            return service;
        }
    }
}
