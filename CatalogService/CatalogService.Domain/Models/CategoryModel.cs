using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public CategoryModel? Parent { get; set; }
        public Guid? ParentId { get; set; }
        public CategoryModel? Child { get; set; }
        public List<ItemModel>? Items { get; set; }
    }
}
