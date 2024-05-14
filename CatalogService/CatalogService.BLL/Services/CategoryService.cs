using CatalogService.BLL.Interfaces;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repo;

        public CategoryService(ICategoryRepository repo)
        {
            this.repo = repo;
        }
        public bool AddCategory(CategoryDtoModel category)
        {
            return repo.AddCategory(category);
        }

        public bool DeleteCategory(Guid id)
        {
            return repo.DeleteCategory(id);
        }

        public List<CategoryDtoModel> GetCategories()
        {
            return repo.GetCategories();
        }

        public CategoryDtoModel GetCategory(Guid id)
        {
            return repo.GetCategory(id);
        }

        public bool UpdateCategory(CategoryDtoModel item)
        {
            return repo.UpdateCategory(item);
        }
    }
}
