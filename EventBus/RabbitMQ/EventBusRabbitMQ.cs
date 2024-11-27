using EventBus.Event;
using EventBus.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ(
        ILogger<EventBusRabbitMQ> _logger,
        IOptions<EventBusOption> options,
        IConfiguration configuration) : IEventBus, IDisposable
    {
        private readonly string _rabbitmqHost = "localhost";
        //private readonly string _queueName = options.Value.SubscriptionClientName;
        private readonly string _queueName = "EmailQueue";
        
        public async Task PublishAsync(MailEvent mailEvent)
        {   
            var factory = new ConnectionFactory { HostName = _rabbitmqHost };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: _queueName, durable: true, exclusive: false, autoDelete: false,
                arguments: null);

            string message = JsonConvert.SerializeObject(mailEvent);
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: _queueName, body: body);
        }

        public void Dispose()
        {
            
        }
    }
}
