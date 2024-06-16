using CatalogSercice.RabbitMq.Interfaces;
using CatalogService.RabbitMq.Models;
using RabbitMQ.Client;
using System.Text;

namespace CatalogSercice.RabbitMq.Services
{
    public class RabbitMq : IRabbitMq
    {
        private IConnection _connection;
        private readonly string hostName;
        private readonly bool persistence;
        private readonly string routingKey;
        private readonly FailedMessageStorage storage;

        public RabbitMq(string hostName, bool persistence, string routingKey, FailedMessageStorage storage)
        {
            this.hostName = hostName;
            this.persistence = persistence;
            this.routingKey = routingKey;
            this.storage = storage;
            CreateConnection();
        }

        public void SendMessage(string message)
        {
            if (ConnectionExists())
            {
                var result = SendToQueue(message);
                int retryCounter = 0;
                while (result && retryCounter < 3)
                {
                    Task.Delay(TimeSpan.FromSeconds(1));
                    result = SendToQueue(message);
                    retryCounter++;
                }
                if (!result)
                    return;

                storage.Messages.Enqueue(message);

            }
        }

        private bool SendToQueue(string message)
        {
            using (var channel = _connection.CreateModel())
            {
                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>()
                                {
                                    { "TestMessage","This Is A Test" }
                                };
                properties.Persistent = persistence;
                var encodedMessage = Encoding.UTF8.GetBytes(message);

                channel.ConfirmSelect();
                channel.BasicPublish(exchange: "", routingKey: routingKey, basicProperties: properties, body: encodedMessage);
                var result = channel.WaitForConfirms();
                return result;
            }
        }


        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = hostName
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }

        public void CheckFailedMessageQueue()
        {
            if (storage.Messages.Count > 0)
            {
                var messageCount = storage.Messages.Count;
                for (int i = 0; i < messageCount; i++)
                {
                    SendMessage(storage.Messages.Dequeue());
                }
            }
        }
    }
}
