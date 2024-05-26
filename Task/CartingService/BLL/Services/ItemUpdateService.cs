using CatingService.BLL.Interfaces;
using System.Text.Json;
using Newtonsoft.Json;
using CartingService.BLL.Models;
using CartingService.DAL.Interfaces;

namespace CatingService.BLL.Services
{
    public class ItemUpdateService : IItemUpdateService
    {
        private readonly IGenericRepository<CartModel> repository;

        public ItemUpdateService(IGenericRepository<CartModel> repository)
        {
            this.repository = repository;
        }

        public void UpdateItems(string message)
        {
            var itemModel = JsonConvert.DeserializeObject<ItemModel>(message);
            var carts = repository.GetAllRecords();
            foreach(var cart in carts)
            {
                if(cart.Items.Any(x=>x.Id == itemModel.Id))
                {
                    var item = cart.Items.FirstOrDefault(x => x.Id == itemModel.Id);
                    cart.Items.Remove(item);
                    item.Name = itemModel.Name;
                    item.Price = itemModel.Price;
                    item.Image = itemModel.Image;
                    cart.Items.Add(item);
                    repository.UpsertRecord(cart);

                }
            }
        }
    }
}
