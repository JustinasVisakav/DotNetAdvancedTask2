using CatalogSercice.Infrastructure.Queue.Models;
using CatalogService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogSercice.Infrastructure.Queue.Interfaces
{
    public interface IRabbitMq
    {
        public bool SendMessage(string message);
    }
}
