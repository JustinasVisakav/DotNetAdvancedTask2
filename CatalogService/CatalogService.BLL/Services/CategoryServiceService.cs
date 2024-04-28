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
    public class CategoryServiceService : ICategoryService
    {
        private readonly ICategoryRepository repo;

        public CategoryServiceService(ICategoryRepository repo)
        {
            this.repo = repo;
        }
        public bool AddCategory(CategoryModel category)
        {
            return repo.AddCategory(category);
        }

        public bool DeleteCategory(Guid id)
        {
            return repo.DeleteCategory(id);
        }

        public List<CategoryModel> GetCategories()
        {
            return repo.GetCategories();
        }

        public CategoryModel GetCategory(Guid id)
        {
            return repo.GetCategory(id);
        }

        public bool UpdateCategory(CategoryModel item)
        {
            return repo.UpdateCategory(item);
        }
    }
}
