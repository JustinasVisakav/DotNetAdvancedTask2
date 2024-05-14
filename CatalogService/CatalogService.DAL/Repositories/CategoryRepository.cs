using CatalogService.DAL.ContextKeeper;
using CatalogService.DAL.Extensions;
using CatalogService.DAL.Interfaces;
using CatalogService.DAL.Models;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CatalogService.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext db;
        private readonly ICategoryMapper categoryMapper;

        public CategoryRepository(DatabaseContext db,ICategoryMapper categoryMapper,IItemMapper itemMapper)
        {
            this.db = db;
            this.categoryMapper = categoryMapper;
        }
        public bool AddCategory(CategoryDtoModel category)
        {
            if (category.Parent != null)
            {
                category.Parent = GetCategory((Guid)category.Parent.Id);
            }
            db.Categories.Add(categoryMapper.CategoryToModel(category));
            var a=db.SaveChanges();
            return true;
        }

        public bool DeleteCategory(Guid id)
        {
            db.Categories.Remove(db.Categories.FirstOrDefault(x=>x.Id == id));
            var a = db.SaveChanges();
            return true;
        }

        public CategoryDtoModel GetCategory(Guid id)
        {
            if (db.Categories.FirstOrDefault(x => x.Id == id) != null)
            {
                return categoryMapper.CategoryToDtoModel(db.Categories.FirstOrDefault(x => x.Id == id));
            }
            return null;
        }

        public List<CategoryDtoModel> GetCategories()
        {
            List<CategoryDtoModel> resultDtoList = new List<CategoryDtoModel>();
            var categories = db.Categories.ToList();
            foreach(var category in categories)
            {
                resultDtoList.Add(categoryMapper.CategoryToDtoModel(category));
            }
            return resultDtoList;
        }

        public bool UpdateCategory(CategoryDtoModel category)
        {
            var categoryToRemove = db.Categories.FirstOrDefault(x => x.Id == category.Id);
            db.Categories.Remove(categoryToRemove);
            db.Categories.Add(categoryMapper.CategoryToModel(category));
            var updateResult = db.SaveChanges();
            return updateResult==1;
        }
    }
}
