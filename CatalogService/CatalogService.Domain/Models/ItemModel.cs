using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Models
{
    public class ItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public CategoryModel? Category { get; set; }
        public Guid? CategoryId { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }

    }
}
