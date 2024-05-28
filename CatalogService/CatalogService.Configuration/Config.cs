using CatalogService.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CatalogService.Configuration
{
    public class Config : IConfig
    {
        private readonly IConfiguration rabbitMqConfig;

        private const string rabbitMqConfigName = "RabbitMq";

        //S3 config
        private const string routingKey = "RoutingKey";
        private const string hostName = "HostName";
        private const string persistense = "Persistence";


        public Config(IConfiguration config)
        {
            rabbitMqConfig = config.GetSection(rabbitMqConfigName);
        }
        public string RoutingKey => GetValue(rabbitMqConfig, routingKey);
        public string HostName => GetValue(rabbitMqConfig, hostName);
        public bool Persistense => GetBoolValue(rabbitMqConfig, persistense);


        private static string GetValue(IConfiguration config, string key)
        {
            string? value = null;

            if (string.IsNullOrWhiteSpace(value))
                value = config.GetSection(key)?.Value;

            return value;
        }

        private static int? GetIntValue(IConfiguration config, string key)
        {
            int? value = null;

            if (value is null)
                value = int.TryParse(config.GetSection(key)?.Value, out int x) ? x : null;

            return value;
        }

        private static bool GetBoolValue(IConfiguration config, string key)
        {
            string? value = null;

            if (string.IsNullOrWhiteSpace(value))
                value = config.GetSection(key)?.Value;

            return value is not null && value.Equals("true", StringComparison.OrdinalIgnoreCase);
        }
    }
}
