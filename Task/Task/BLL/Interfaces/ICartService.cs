using Task.BLL.Models;

namespace Task.BLL.Interfaces
{
    public interface ICartService
    {
        public Guid CreateNewCart();
        public bool AddToCartCart(Guid id, List<ItemModel> items);
        public bool RemoveFromCart(Guid id, List<ItemModel> items);
        public CartModel GetCart(Guid id);
    }
}
