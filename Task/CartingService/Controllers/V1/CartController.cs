using Asp.Versioning;
using CartingService.BLL.Interfaces;
using CartingService.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatingService.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService service;

        public CartController(ICartService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Used to create new empty cart
        /// </summary>
        /// <returns>CartId on success</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult Create()
        {
            var cartId = service.CreateNewCart();
            if (string.IsNullOrEmpty(cartId.ToString()))
                return StatusCode(500);

            return Ok(cartId);
        }

        /// <summary>
        /// Used to add item to cart
        /// </summary>
        /// <param name="id"></param>
        /// <param name="items"></param>
        /// <returns>Boolean if operation was successful</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult AddToCart(Guid id, [FromBody] List<ItemModel> items)
        {
            var result = service.AddToCartCart(id, items);

            if (!result)
                return StatusCode(500);

            return Ok(result);
        }

        /// <summary>
        /// Used to delete items from cart
        /// </summary>
        /// <param name="id"></param>
        /// <param name="items"></param>
        /// <returns>Returns boolean of if operation was successful</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id, [FromBody] List<ItemModel> items)
        {
            var result = service.RemoveFromCart(id, items);

            if (!result)
                return StatusCode(500);

            return Ok(result);
        }


        /// <summary>
        /// Gets cart model
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Cart</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CartModel), 200)]
        public IActionResult Get(Guid id)
        {
            var result = service.GetCart(id);

            if (result == null)
                return StatusCode(500);

            return Ok(result);
        }
    }
}
