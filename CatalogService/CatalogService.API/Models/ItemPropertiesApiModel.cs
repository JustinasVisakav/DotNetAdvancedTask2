using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Models
{
    public class ItemPropertiesApiModel
    {
        public Dictionary<string, string> Properties { get; set; } = new();
    }
}
