using Asp.Versioning;
using CartingService.BLL.Interfaces;
using CartingService.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatingService.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v2/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService service;

        public CartController(ICartService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult Get()
        {
            var cartId = service.CreateNewCart();
            if (string.IsNullOrEmpty(cartId.ToString()))
                return StatusCode(500);

            return Ok(cartId);
        }

        [HttpPut("{id}")]
        public IActionResult AddToCart(Guid id, [FromBody] List<ItemModel> items)
        {
            var result = service.AddToCartCart(id, items);

            if (!result)
                return StatusCode(500);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id, [FromBody] List<ItemModel> items)
        {
            var result = service.RemoveFromCart(id, items);

            if (!result)
                return StatusCode(500);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<ItemModel>), 200)]
        public IActionResult Get(Guid id)
        {
            var result = service.GetCart(id).Items;

            if (result == null)
                return StatusCode(500);

            return Ok(result);
        }
    }
}
