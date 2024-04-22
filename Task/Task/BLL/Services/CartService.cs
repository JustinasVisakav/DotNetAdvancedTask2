using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.BLL.Interfaces;
using Task.BLL.Models;
using Task.DAL.Interfaces;
using Task.DAL.Repositories;

namespace Task1.BLL.Services
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
            repository.UpsertRecord(cart);
            return true;
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
            repository.UpsertRecord(cart);
            return true;
        }
    }
}
