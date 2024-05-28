using CatalogSercice.RabbitMq.Interfaces;
using CatalogSercice.RabbitMq.Models;
using CatalogService.BLL.Interfaces;
using CatalogService.BLL.Wrappers;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Models;
using System.Text.Json;

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
                rabbit.SendMessage(PrepareItemForMessaging(item));
                return updateSuccessful;
            }
            return false;
        }


        private string PrepareItemForMessaging(ItemDtoModel item)
        {
            var transferModel = TransformItem(item);
            return JsonSerializer.Serialize(transferModel);
        }

        private TransferModel TransformItem(ItemDtoModel model)
        {
            var transferModel = new TransferModel();
            transferModel.Name = model.Name;
            transferModel.Price = model.Price;
            transferModel.Id = model.Id;
            transferModel.Image = model.Image;
            return transferModel;
        }
    }
}
