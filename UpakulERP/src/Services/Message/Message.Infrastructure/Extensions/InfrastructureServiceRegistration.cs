using Message.Infrastructure.Persistence;
using Message.Infrastructure.Repository.Interfaces;
using Message.Infrastructure.Repository.Strategies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Message.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddMessageInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MessageDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("MessageDBConnection")));
            services.AddScoped<IUserMailLogRepository, UserMailLogRepository>();
            return services;
        }
    }
}
