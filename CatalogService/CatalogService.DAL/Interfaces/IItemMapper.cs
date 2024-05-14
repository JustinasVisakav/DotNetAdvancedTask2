using CatalogService.DAL.Models;
using CatalogService.Domain.Models;

namespace CatalogService.DAL.Interfaces
{
    public interface IItemMapper
    {
        public ItemDtoModel ItemToItemDtoModel(ItemModel itemModel);
        public ItemModel ItemToModel(ItemDtoModel itemDtoModel);
    }
}
