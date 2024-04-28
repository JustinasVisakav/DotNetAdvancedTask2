using Microsoft.AspNetCore.Mvc;
using CartingService.BLL.Interfaces;
using CartingService.BLL.Models;

namespace CartingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService service;

        public CartController(ICartService service)
        {
            this.service = service;
        }

        [HttpPost(Name = "Create cart")]
        public IActionResult Get()
        {
            
            var cartId = service.CreateNewCart();
            return Ok(cartId);
        }

        [HttpPut("{id}")]
        public IActionResult AddToCart(Guid id)
        {
            ItemModel model = new ItemModel();
            List<ItemModel> items = new();
            items.Add(model);
            service.AddToCartCart(id, items);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var cart = service.GetCart(id);
            List<ItemModel> items = new();
            items.Add(cart.Items.FirstOrDefault());

            service.RemoveFromCart(id, items);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(service.GetCart(id));
        }
    }
}
