using CatalogService.DAL.ContextKeeper;
using CatalogService.DAL.Interfaces;
using CatalogService.DAL.Models;
using CatalogService.Domain.Models;

namespace CatalogService.DAL.Extensions
{
    public class CategoryMapper:ICategoryMapper
    {
        private readonly DatabaseContext db;

        public CategoryMapper(DatabaseContext db)
        {
            this.db = db;
        }

        public CategoryDtoModel CategoryToDtoModel(CategoryModel categoryModel)
        {
            CategoryDtoModel categoryDtoModel = new CategoryDtoModel()
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name,
                Image = categoryModel.Image,
            };

            if(categoryModel.Items != null)
            {
                categoryDtoModel.Items = new();
                foreach(var item in categoryModel.Items)
                {
                    var itemFromDb = db.Items.FirstOrDefault(x => x.Id == item);
                    if (itemFromDb != null)
                    {
                        categoryDtoModel.Items.Add(new()
                        {
                            Id = itemFromDb.Id,
                            Amount = itemFromDb.Amount,
                            Name = itemFromDb.Name,
                            Description = itemFromDb.Description,
                            Image = itemFromDb.Image,
                            Price = itemFromDb.Price
                        });
                    }
                }
            }
            if(categoryModel.ParentId != null)
            {
                var parent = db.Categories.FirstOrDefault(x => x.Id == categoryModel.ParentId);
                categoryDtoModel.Parent = new()
                {
                    Id = parent.Id,
                    Name = parent.Name,
                    Image = parent.Image,
                };
            }

            return categoryDtoModel;
        }

        public CategoryModel CategoryToModel(CategoryDtoModel categoryDtoModel)
        {
            CategoryModel model = new CategoryModel()
            {
                Id = categoryDtoModel.Id,
                Name = categoryDtoModel.Name,
                Image = categoryDtoModel.Image,
                
            };

            if(categoryDtoModel.Parent != null)
            {
                model.ParentId = categoryDtoModel.Parent.Id;
            }

            if(categoryDtoModel.Items != null)
            {
                model.Items = new();
                foreach(var item in categoryDtoModel.Items)
                {
                    model.Items.Add(item.Id);
                }
            }

            return model;
        }
    }
}
