using CatalogService.BLL.Interfaces;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Models;

namespace CatalogService.BLL.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository repo;

        public ItemService(IItemRepository repo)
        {
            this.repo = repo;
        }
        public bool AddItem(ItemDtoModel item)
        {
            return repo.AddItem(item);
        }

        public bool DeleteItem(Guid id)
        {
            return repo.DeleteItem(id);
        }

        public ItemDtoModel GetItem(Guid id)
        {
            return repo.GetItem(id);
        }

        public List<ItemDtoModel> GetItems()
        {
            return repo.GetItems();
        }

        public bool UpdateItem(ItemDtoModel item)
        {
            return repo.UpdateItem(item);
        }
    }
}
