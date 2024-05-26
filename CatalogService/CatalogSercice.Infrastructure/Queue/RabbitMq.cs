using CatalogSercice.Infrastructure.Queue.Interfaces;
using CatalogSercice.Infrastructure.Queue.Models;
using CatalogService.Domain.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatalogSercice.Infrastructure.Queue
{
    public class RabbitMq : IRabbitMq
    {
        public bool SendMessage(ItemDtoModel model)
        {
            var transferModel = new TransferModel(model);
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>()
                {
                    { "TestMessage","This Is A Test" }
                };
                properties.Persistent = true;
                var serializedModel = JsonSerializer.Serialize(transferModel);
                var message = Encoding.UTF8.GetBytes(serializedModel);

                channel.BasicPublish(exchange: "", routingKey: "ItemUpdateQueue", basicProperties: properties, body: message);

                return true;
            }
        }
    }
}
