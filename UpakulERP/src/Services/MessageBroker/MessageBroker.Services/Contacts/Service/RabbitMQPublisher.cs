using System.Text;
using MessageBroker.Services.Contacts.Persistence;
using MessageBroker.Services.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace MessageBrokerServices.Contacts.Service
{
    internal class RabbitMQPublisher<T> : IRabbitMQPublisher<T>
    {
        private readonly RabbitMQCredential _rabbitMqSetting;
        public RabbitMQPublisher(IOptions<RabbitMQCredential> rabbitMqSetting)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;
        }
        public async Task PublishMessageAsync(T message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSetting.HostName,
                UserName = _rabbitMqSetting.UserName,
                Password = _rabbitMqSetting.Password
            };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: true, arguments: null);
            var messageJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageJson);
            await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);
        }

    }
}
