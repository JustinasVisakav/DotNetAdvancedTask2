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
        private readonly ILogger<CartController> logger;
        private const string controllerName = nameof(CartController) + " V1";

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
            logger.LogInformation($"Location: {controllerName}, request Create new cart");
            var cartId = service.CreateNewCart();
            if (string.IsNullOrEmpty(cartId.ToString()))
            {
                logger.LogInformation($"Location: {controllerName}, request for new cart failed");
                return StatusCode(500);
            }
            logger.LogInformation($"Location: {controllerName}, request for new cart success");
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
        public IActionResult Delete(Guid id, [FromBody] List<ItemModel> items)
        {
            logger.LogInformation($"Location: {controllerName}, request {id} delete cart");
            var result = service.RemoveFromCart(id, items);

            if (!result)
            {
                logger.LogInformation($"Location: {controllerName}, request {id} delete cart failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request {id} delete cart success");
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
            logger.LogInformation($"Location: {controllerName}, request {id} get cart");
            var result = service.GetCart(id);

            if (result == null)
            {
                logger.LogInformation($"Location: {controllerName}, request {id} get cart Failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request {id} get cart Success");
            return Ok(result);
        }
    }
}
