using Auth.API.Repositories.Interfaces;
using Auth.API.Repositories.Strategies;
using Auth.API.Services;

namespace Auth.API.Extensions
{
    public static class DependencyInjectionRepositories
    {
        public static IServiceCollection AddDependencyInjectionRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeStrategy, EmployeeStrategy>();
            services.AddScoped<IUserStrategy, UserStrategy>();
            services.AddScoped<IModuleStrategy, ModuleStrategy>();
            services.AddScoped<IMenuStrategy, MenuStrategy>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleXMenuStrategy, RoleXMenuStrategy>();
            services.AddScoped<IRoleXModuleStrategy, RoleXModuleStrategy>();
            services.AddScoped<IMFTransactionDateStrategy, MFTransactionDateStrategy>();
            services.AddScoped<INotificationStrategy, NotificationStrategy>();
            services.AddScoped<IJsonDataGenerate, JsonDataGenerate>();

            return services;
        }
    }
}
