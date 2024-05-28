using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.RabbitMq.Models
{
    public class FailedMessageStorage
    {
        public Queue<string> Messages { get; set; } = new Queue<string>();
    }
}
