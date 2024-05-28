using CatalogSercice.Infrastructure.Queue.Interfaces;
using CatalogSercice.Infrastructure.Queue.Models;
using CatalogService.Domain.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace CatalogSercice.Infrastructure.Queue
{
    public class RabbitMq : IRabbitMq
    {
        private IConnection _connection;
        private readonly string hostName;
        private readonly bool persistence;
        private readonly string routingKey;

        public RabbitMq(string hostName, bool persistence, string routingKey)
        {
            this.hostName = hostName;
            this.persistence = persistence;
            this.routingKey = routingKey;

            CreateConnection();
        }

        public bool SendMessage(string message)
        {
            if (ConnectionExists())
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
                    channel.WaitForConfirmsOrDie();
                    channel.ConfirmSelect();

                    return true;
                }
            }
            return false;
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
    }
}
