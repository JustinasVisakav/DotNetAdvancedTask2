using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ChildId { get; set; }
        public List<Guid>? Items { get; set; }
    }
}
