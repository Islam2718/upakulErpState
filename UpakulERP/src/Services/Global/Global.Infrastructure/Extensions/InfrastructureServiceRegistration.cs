using Global.Application.Contacts.Persistence;
using Global.Infrastructure.Persistence;
using Global.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Global.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IGeoLocationRepository, GeoLocationRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();

            return services;
        }
    }
}
