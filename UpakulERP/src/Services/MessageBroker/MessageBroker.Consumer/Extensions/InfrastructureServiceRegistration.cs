using MessageBroker.Consumer.MessageConsumer;
using MessageBroker.Services.Domain;

namespace MessageBroker.Consumer.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWindowsService(options => { options.ServiceName = "Message Queue Consumer Service"; });
            services.Configure<ConnectionModel>(configuration.GetSection("ConnectionStrings"));
            // Consumer Message
            services.AddHostedService<BankMessageConsumer>();
            services.AddHostedService<OfficeMessageConsumer>();
            services.AddHostedService<GeoLocationMessageConsumer>();
            services.AddHostedService<DesignationMessageConsumer>();
            services.AddHostedService<EmployeeMessageConsumer>();
            return services;
        }
    }
}
