using CatalogSercice.Infrastructure.Queue.Interfaces;
using CatalogService.BLL.Interfaces;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Models;

namespace CatalogService.BLL.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository repo;
        private readonly IRabbitMq rabbit;

        public ItemService(IItemRepository repo, IRabbitMq rabbit)
        {
            this.repo = repo;
            this.rabbit = rabbit;
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
            var updateSuccessful = repo.UpdateItem(item);
            if (updateSuccessful)
            {
                rabbit.SendMessage(item);
                return updateSuccessful;
            }
            return false;
        }
    }
}
