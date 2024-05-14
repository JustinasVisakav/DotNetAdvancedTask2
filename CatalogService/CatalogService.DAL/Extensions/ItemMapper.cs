using CatalogService.DAL.ContextKeeper;
using CatalogService.DAL.Interfaces;
using CatalogService.DAL.Models;
using CatalogService.Domain.Models;

namespace CatalogService.DAL.Extensions
{
    public class ItemMapper:IItemMapper
    {
        private readonly DatabaseContext db;

        public ItemMapper(DatabaseContext db)
        {
            this.db = db;
        }

        public ItemDtoModel ItemToItemDtoModel(ItemModel itemModel)
        {
            ItemDtoModel dtoModel = new ItemDtoModel()
            {
                Id = itemModel.Id,
                Name = itemModel.Name,
                Amount = itemModel.Amount,
                Description = itemModel.Description,
                Image = itemModel.Image,
                Price = itemModel.Price
            };

            if (itemModel.CategoryId != null)
            {
                var category = db.Categories.FirstOrDefault(c => c.Id == itemModel.CategoryId);
                dtoModel.Category = new()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Image = category.Image
                };
            }

            return dtoModel;
        }

        public ItemModel ItemToModel(ItemDtoModel itemDtoModel)
        {
            ItemModel model = new ItemModel()
            {
                Id = itemDtoModel.Id,
                Name = itemDtoModel.Name,
                Amount = itemDtoModel.Amount,
                Price = itemDtoModel.Price,
                Description = itemDtoModel.Description,
                Image = itemDtoModel.Image,

            };

            if (itemDtoModel.Category != null)
            {
                model.CategoryId = itemDtoModel.Category.Id;
            }

            return model;
        }
    }
}
