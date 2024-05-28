using CatalogService.Configuration.Interfaces;
using CatalogService.RabbitMq.Models;

namespace CatalogService.BLL.Wrappers
{
    public class RabbitMqWrapper : CatalogSercice.RabbitMq.Services.RabbitMq
    {
        public RabbitMqWrapper(IConfig config, FailedMessageStorage storage) : base(config.HostName, config.Persistense, config.RoutingKey, storage) { }
    }
}
