using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogSercice.RabbitMq.Interfaces
{
    public interface IRabbitMq
    {
        public void SendMessage(string message);
        public void CheckFailedMessageQueue();
    }
}
