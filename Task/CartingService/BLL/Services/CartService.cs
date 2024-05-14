using CartingService.BLL.Interfaces;
using CartingService.BLL.Models;
using CartingService.DAL.Interfaces;

namespace CartingService.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IGenericRepository<CartModel> repository;

        public CartService(IGenericRepository<CartModel> repository)
        {
            this.repository = repository;
        }

        public bool AddToCartCart(Guid id, List<ItemModel> items)
        {
            var cart = repository.GetRecord(id);
            cart.Items.AddRange(items);
            return repository.UpsertRecord(cart);
        }

        public Guid CreateNewCart()
        {
            CartModel model = new CartModel();
            repository.UpsertRecord(model);
            return model.Id;
        }

        public CartModel GetCart(Guid id)
        {
            return repository.GetRecord(id);
        }

        public bool RemoveFromCart(Guid id, List<ItemModel> items)
        {
            var cart = repository.GetRecord(id);
            foreach(var item in items)
            {
                cart.Items.Remove(cart.Items.FirstOrDefault(x=>x.Id == item.Id));
            }
            
            return repository.UpsertRecord(cart);
        }
    }
}
