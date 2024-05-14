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
        public CategoryDtoModel GetCategory(Guid id);
        public List<CategoryDtoModel> GetCategories();
        public bool AddCategory(CategoryDtoModel category);
        public bool UpdateCategory(CategoryDtoModel item);
        public bool DeleteCategory(Guid id);
    }
}
