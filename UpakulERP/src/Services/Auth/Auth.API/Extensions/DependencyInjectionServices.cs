using Auth.API.Repositories.Interfaces;
using Auth.API.Repositories.Strategies;
using Auth.API.Services;

namespace Auth.API.Extensions
{
    public static class DependencyInjectionServices
    {
        public static IServiceCollection AddDependencyInjectionServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
