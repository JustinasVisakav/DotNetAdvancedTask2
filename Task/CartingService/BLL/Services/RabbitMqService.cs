using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using CatingService.BLL.Interfaces;

namespace CatingService.BLL.Services
{
    public class RabbitMqService : IHostedService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "ItemUpdateQueue";
        private readonly IItemUpdateService updateService;

        public RabbitMqService(IItemUpdateService updateService)
        {
            this.updateService = updateService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received message: {0}", message);
                updateService.UpdateItems(message);

            };

            _channel.BasicConsume(queue: _queueName,
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}
