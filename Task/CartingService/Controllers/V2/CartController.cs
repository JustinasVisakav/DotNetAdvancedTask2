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
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult Delete(Guid id, [FromBody] List<ItemModel> items)
        {
            var result = service.RemoveFromCart(id, items);

            if (!result)
                return StatusCode(500);

            return Ok(result);
        }

        /// <summary>
        /// Gets items from specified cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of items in the cart</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<ItemModel>), 200)]
        public IActionResult Get(Guid id)
        {
            var result = service.GetCart(id).Items;

            if (result == null)
                return StatusCode(500);

            return Ok(result);
        }

        /// <summary>
        /// Gets all cats
        /// </summary>
        /// <returns>List of items in the cart</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CartModel>), 200)]
        public IActionResult Get()
        {
            var result = service.GetCarts();

            if (result == null)
                return StatusCode(500);

            return Ok(result);
        }
    }
}
