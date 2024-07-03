using Asp.Versioning;
using CartingService.BLL.Interfaces;
using CartingService.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatingService.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v2/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService service;
        private readonly ILogger<CartController> logger;
        private const string controllerName = nameof(CartController)+" V2";

        public CartController(ICartService service, ILogger<CartController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        /// <summary>
        /// Used to create new empty cart
        /// </summary>
        /// <returns>CartId on success</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult Create()
        {
            logger.LogInformation($"Location: {controllerName}, request create new cart");
            var cartId = service.CreateNewCart();
            if (string.IsNullOrEmpty(cartId.ToString()))
            {
                logger.LogInformation($"Location: {controllerName}, request create new cart Failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request create new cart Success");
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
            logger.LogInformation($"Location: {controllerName}, request {id} modify cart");
            var result = service.AddToCartCart(id, items);

            if (!result)
            {
                logger.LogInformation($"Location: {controllerName}, request {id} modify cart Failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request {id} modify cart Success");
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
            logger.LogInformation($"Location: {controllerName}, request {id} delete cart");
            var result = service.RemoveFromCart(id, items);

            if (!result)
            {
                logger.LogInformation($"Location: {controllerName}, request {id} delete cart Failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request {id} dekete cart Success");
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
            logger.LogInformation($"Location: {controllerName}, request {id} get cart");
            var result = service.GetCart(id).Items;

            if (result == null)
            {
                logger.LogInformation($"Location: {controllerName}, request {id} get cart Failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request {id} get cart Success");
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
            logger.LogInformation($"Location: {controllerName}, request Get all carts");
            var result = service.GetCarts();

            if (result == null)
            {
                logger.LogInformation($"Location: {controllerName}, request Get all carts Failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request Get all carts Success");
            return Ok(result);
        }
    }
}
