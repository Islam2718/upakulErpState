using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace MessageBroker.Services.Contacts.Persistence
{
    public interface IConsuningService<T>
    {
        Task<T> StartConsuming(IChannel _channel, IServiceProvider _serviceProvider, string queueName, CancellationToken cancellationToken);
    }
}
