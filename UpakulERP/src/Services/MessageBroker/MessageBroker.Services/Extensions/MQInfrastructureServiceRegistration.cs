using MessageBrokerServices.Contacts.Service;
using MessageBroker.Services.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MessageBroker.Services.Contacts.Persistence;

namespace MessageBroker.Services.Extensions
{
    public static class MQInfrastructureServiceRegistration
    {
        public static IServiceCollection AddMQInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile("RabbitMQSetting.json",optional:false,reloadOnChange:true).Build();
            services.Configure<RabbitMQCredential>(configuration.GetSection("RabbitMQ"));
            services.Configure<PublisherStatus>(configuration.GetSection("PublisherStatus"));
            services.AddScoped(typeof(IRabbitMQPublisher<>), typeof(RabbitMQPublisher<>));
            return services;
        }
    }
}
