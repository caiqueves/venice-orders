using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using Venice.Orders.Application.Interfaces;
using Venice.Orders.CrossCutting.Shareable.Config;

namespace Venice.Orders.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMqEventPublisher : IRabbitMqEventPublisher
    {
        private readonly string _queueName;

        private readonly ConnectionFactory _factory;

        public RabbitMqEventPublisher(IOptions<AppConfig> opt)
        {
            _queueName = opt.Value.RabbitMqSettings.Queue;

            var encodedPass = Uri.EscapeDataString(opt.Value.RabbitMqSettings.Password);

            var uri = new Uri($"amqp://{opt.Value.RabbitMqSettings.UserName}:{encodedPass}@{ opt.Value.RabbitMqSettings.HostName}");

            _factory = new ConnectionFactory
            {
                Uri = uri,
                AutomaticRecoveryEnabled = true
            };
        }

        public async Task PublishEvent(string eventMessage)
        {
            var connection = await _factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();



            await channel.QueueDeclareAsync(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(eventMessage);


            await channel.BasicPublishAsync(exchange: "",
                                 routingKey: _queueName,
                                 body: body);



            Console.WriteLine($"{eventMessage}");
        }
    }
}
