using CatalogService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        public CategoryModel GetCategory(Guid id);
        public List<CategoryModel> GetCategories();
        public bool AddCategory(CategoryModel category);
        public bool UpdateCategory(CategoryModel item);
        public bool DeleteCategory(Guid id);
    }
}
