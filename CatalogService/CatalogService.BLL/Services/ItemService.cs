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
        public bool AddItem(ItemModel item)
        {
            return repo.AddItem(item);
        }

        public bool DeleteItem(Guid id)
        {
            return repo.DeleteItem(id);
        }

        public ItemModel GetItem(Guid id)
        {
            return repo.GetItem(id);
        }

        public List<ItemModel> GetItems()
        {
            return repo.GetItems();
        }

        public bool UpdateItem(ItemModel item)
        {
            return repo.UpdateItem(item);
        }
    }
}
