using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Contacts.Persistence;
using Project.Infrastructure.Persistence;
using Project.Infrastructure.Repository;

namespace Project.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IDonerRepository, DonerRepository>();

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IActivityPlanRepository, ActivityPlanRepository>();
            return services;
        }
    }
}
