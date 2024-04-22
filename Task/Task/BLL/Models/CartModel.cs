
namespace Task.BLL.Models
{
    public class CartModel
    {
        public Guid Id { get; set; }
        public List<ItemModel> Items { get; set; }

        public CartModel()
        {
            Id = Guid.NewGuid();
            Items = new List<ItemModel>();
        }
    }
}
