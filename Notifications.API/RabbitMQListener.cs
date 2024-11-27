using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using Notifications.API.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Notifications.API
{
    public class RabbitMqListener(ILogger<RabbitMqListener> _logger,
        IEmailService emailService) : IHostedService
    {
        
        private readonly string _queueName = "EmailQueue";

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: _queueName, durable: true, exclusive: false, autoDelete: false,
                arguments: null);

            _logger.LogInformation(" [*] Waiting for messages.");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var messageObject = JsonConvert.DeserializeObject<NotificationMail>(json);
                _logger.LogInformation("Received message: {0}", messageObject?.Body);
                emailService.SendEmail(messageObject);
                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(_queueName, autoAck: true, consumer: consumer);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}
