using CatalogService.DAL.ContextKeeper;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext db;

        public CategoryRepository(DatabaseContext db)
        {
            this.db = db;
        }
        public bool AddCategory(CategoryModel category)
        {
            if (category.ParentId != null)
            {
                category.Parent = GetCategory((Guid)category.ParentId);
            }
            db.Categories.Add(category);
            var a=db.SaveChanges();
            return true;
        }

        public bool DeleteCategory(Guid id)
        {
            db.Categories.Remove(GetCategory(id));
            var a = db.SaveChanges();
            return true;
        }

        public CategoryModel GetCategory(Guid id)
        {
            return db.Categories.FirstOrDefault(x => x.Id == id);
        }

        public List<CategoryModel> GetCategories()
        {
            return db.Categories.Include(x => x.Parent).ToList();
        }

        public bool UpdateCategory(CategoryModel item)
        {
            db.Categories.Update(item);
            var a = db.SaveChanges();
            return true;
        }
    }
}
