using MessageBrokerServices.Contacts.Service;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Utility.Domain.DBDomain;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using MessageBroker.Services.Settings;
using MessageBroker.Services.Constants;
using MessageBroker.Services.Domain;
using MessageBroker.Services.Contacts.Service.DBService;

namespace MessageBroker.Consumer.MessageConsumer
{
    public class EmployeeMessageConsumer : BackgroundService, IDisposable
    {
        #region Var
        private readonly ILogger<EmployeeMessageConsumer> _logger;
        private readonly ConnectionModel _connectionString;
        private ConnectionFactory factory;
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IChannel _channel;
        #endregion Var

        public EmployeeMessageConsumer(IOptions<RabbitMQCredential> rabbitMqSetting, IOptions<ConnectionModel> connectionString, IServiceProvider serviceProvider, ILogger<EmployeeMessageConsumer> logger)
        {
            factory = new ConnectionFactoryService().Get(rabbitMqSetting.Value);
            _serviceProvider = serviceProvider;
            _logger = logger;
            _connectionString = connectionString.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            StartConsuming(RabbitMQQueues.Employee, stoppingToken);
            await Task.CompletedTask;
        }

        private async Task StartConsuming(string queueName, CancellationToken cancellationToken)
        {
            await _channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: true, arguments: null);
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                try
                {
                    bool processedSuccessfully = false;
                    processedSuccessfully = await ProcessMessageAsync(message);
                    if (processedSuccessfully)
                        await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                    else
                        await _channel.BasicRejectAsync(deliveryTag: ea.DeliveryTag, requeue: true);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred while processing message from queue {queueName}: {ex}");
                }
            };
            await _channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
        }
        private async Task<bool> ProcessMessageAsync(string message)
        {
            try
            {
                //if (cancellationToken.IsCancellationRequested) { }
                using IServiceScope scope = _serviceProvider.CreateScope();


                var obj = JsonConvert.DeserializeObject<CommonEmployee>(message);
                if (obj != null)
                {
                    //var acc_Status = await new EmployeeService(_connectionString.AccountConnection).Add(obj);
                    //if (acc_Status == "")
                    //    _logger.LogInformation("Office Add in Account Module Successfully.");
                    //else _logger.LogWarning(acc_Status);
                    if (obj.OfficeId != 2)
                    {
                        var mf_Status = await new EmployeeService(_connectionString.MFConnection).Add(obj);
                        if (mf_Status == "")
                            _logger.LogInformation("Employee Add in MF Module Successfully");
                        else _logger.LogWarning(mf_Status);
                    }


                    var fxa_Status = await new EmployeeService(_connectionString.FixedAssetConnection).Add(obj);
                    if (fxa_Status == "")
                        _logger.LogInformation("Employee Add in MF Module Successfully");
                    else _logger.LogWarning(fxa_Status);

                    if (obj.OfficeId == 2)
                    {
                        var proj_Status = await new EmployeeService(_connectionString.ProjectConnection).Add(obj);
                        if (proj_Status == "")
                            _logger.LogInformation("Employee Add in Project Module Successfully.");
                        else _logger.LogWarning(proj_Status);
                    }

                    return true;
                }
                else return false;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing message: {ex.Message}");
                return false;
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await base.StopAsync(stoppingToken);
        }
        public override void Dispose()
        {
            _channel.CloseAsync();
            _connection.CloseAsync();
            base.Dispose();
        }
    }
}
