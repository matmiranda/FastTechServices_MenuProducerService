using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MenuProducerService.Infrastructure.MessageBroker
{
    public class RabbitMQProducer : IRabbitMQProducer, IDisposable
    {
        private readonly RabbitMQSettings _config;
        private IConnection? _connection;
        private IModel? _channel;
        private bool _disposed;

        public RabbitMQProducer(IOptions<RabbitMQSettings> options)
        {
            _config = options.Value;
            InitConnection();
        }

        private void InitConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _config.Host,
                UserName = _config.Username,
                Password = _config.Password,
                DispatchConsumersAsync = true,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task PublishAsync(string queueName, object message)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(RabbitMQProducer));

            //Se conexão ou canal estiverem fechados, tenta reabrir
            if (_connection is null || !_connection.IsOpen || _channel is null || !_channel.IsOpen)
            {
                InitConnection();
            }

            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);

            var json = JsonSerializer.Serialize(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var body = Encoding.UTF8.GetBytes(json);

            var props = _channel.CreateBasicProperties();
            props.ContentType = "application/json";
            props.Persistent = true; //Garante entrega em caso de crash

            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: props, body: body);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_disposed) return;

            _channel?.Close();
            _connection?.Close();
            _channel?.Dispose();
            _connection?.Dispose();

            _disposed = true;
        }
    }
}