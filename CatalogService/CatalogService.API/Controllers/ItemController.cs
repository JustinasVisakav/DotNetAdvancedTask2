using CatalogService.API.Models;
using CatalogService.BLL.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService service;
        private readonly ICategoryService categoryService;
        private readonly ILogger<ItemController> logger;
        private const string controllerName = "ItemController";

        public ItemController(IItemService service, ICategoryService categoryService, ILogger<ItemController> logger)
        {
            this.service = service;
            this.categoryService = categoryService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets all the items or items belonging to specific category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>List of items</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ItemApiModel>), 200)]
        public IActionResult Get([FromQuery] Guid categoryId)
        {
            logger.LogInformation($"Location: {controllerName}, request for items arrived. Additional info: {categoryId}");

            if (categoryId == Guid.Empty)
            {
                var items = service.GetItems();
                if (items == null)
                {
                    logger.LogInformation($"Location: {controllerName}, request for items Failed");
                    return StatusCode(500);
                }
                logger.LogInformation($"Location: {controllerName}, request for items Success");
                return Ok(items.Select(x => x.ToApiModel()).ToList());
            }

            var result = categoryService.GetCategory(categoryId).Items.Select(x => x.ToApiModel()).ToList();
            if (result == null)
            {
                logger.LogInformation($"Location: {controllerName}, request for items Failed. Additional info: {categoryId}");
                return StatusCode(500);
            }
            logger.LogInformation($"Location: {controllerName}, request for items Success. Additional info: {categoryId}");
            return Ok(result);
        }

        /// <summary>
        /// Gets specific item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Item</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemApiModel), 200)]
        public IActionResult GetById(Guid id)
        {
            logger.LogInformation($"Location: {controllerName}, request {id} arrived for get");
            var result = service.GetItem(id);
            if (result == null)
            {
                logger.LogInformation($"Location: {controllerName}, request {id} Failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request {id} Success");
            return Ok(result.ToApiModel());
        }

        /// <summary>
        /// Deletes item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Boolean of operation success</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult DeleteById(Guid id)
        {
            logger.LogInformation($"Location: {controllerName}, request {id} arrived for deletion");
            if (id == Guid.Empty)
            {
                logger.LogInformation($"Location: {controllerName}, request {id} Bad Request");
                return BadRequest();
            }
            var result = service?.DeleteItem(id);
            if (result == null)
            {
                logger.LogInformation($"Location: {controllerName}, request {id} Failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request {id} Success");
            return Ok(result);
        }

        /// <summary>
        /// Creates new item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Boolean of operation success</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult AddItem([FromBody] ItemDtoModel item)
        {
            logger.LogInformation($"Location: {controllerName}, request {item.Id} arrived for add");

            if (item == null)
            {
                logger.LogInformation($"Location: {controllerName}, request {item.Id} Bad Request");
                return BadRequest();
            }

            var result = service.AddItem(item);
            if (!result)
            {
                logger.LogInformation($"Location: {controllerName}, request {item.Id} Failed");
                return StatusCode(500);
            }
            logger.LogInformation($"Location: {controllerName}, request {item.Id} Success");
            return Ok(result);
        }

        /// <summary>
        /// Modifies specific item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Boolean of operation success</returns>
        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult UpdateItem([FromBody] ItemDtoModel item)
        {
            logger.LogInformation($"Location: {controllerName}, request {item.Id} arrived for modification");
            if (item == null)
            {
                logger.LogInformation($"Location: {controllerName}, request {item.Id} bad request");
                return BadRequest();
            }

            var result = service.UpdateItem(item);
            if (!result)
            {
                logger.LogInformation($"Location: {controllerName}, request {item.Id} failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request {item.Id} success");
            return Ok(result);
        }

        /// <summary>
        /// Returns items protperties
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/properties/{id}")]
        [ProducesResponseType(typeof(ItemPropertiesApiModel), 200)]
        public IActionResult GetPropertiesById(Guid id)
        {
            logger.LogInformation($"Location: {controllerName}, request {id} arrived for properties");
            var result = new ItemPropertiesApiModel();
            result.Properties.Add("key", "value");
            if (result == null)
            {
                logger.LogInformation($"Location: {controllerName}, request {id} failed");
                return StatusCode(500);
            }
            logger.LogInformation($"Location: {controllerName}, request {id} success");
            return Ok(result);
        }
    }
}
