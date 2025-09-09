using MessageBroker.Services.Settings;
using RabbitMQ.Client;

namespace MessageBrokerServices.Contacts.Service
{
    public class ConnectionFactoryService
    {
        public ConnectionFactory Get(RabbitMQCredential setting)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = setting.HostName,
                UserName = setting.UserName,
                Password = setting.Password,
            };
            return factory;
        }
    }
}
