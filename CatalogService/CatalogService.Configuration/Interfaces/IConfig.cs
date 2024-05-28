using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Configuration.Interfaces
{
    public interface IConfig
    {
        public string RoutingKey { get; }
        public string HostName { get; }
        public bool Persistense { get; }
    }
}
