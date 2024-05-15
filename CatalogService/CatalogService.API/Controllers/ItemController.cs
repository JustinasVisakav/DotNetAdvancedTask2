using CatalogService.API.Models;
using CatalogService.BLL.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService service;
        private readonly ICategoryService categoryService;

        public ItemController(IItemService service, ICategoryService categoryService)
        {
            this.service = service;
            this.categoryService = categoryService;
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
            if (categoryId == Guid.Empty)
            {
                var items = service.GetItems();
                if (items == null)
                {
                    return StatusCode(500);
                }
                return Ok(items.Select(x=>x.ToApiModel()).ToList());
            }

            var result = categoryService.GetCategory(categoryId).Items.Select(x=>x.ToApiModel()).ToList();
            if (result == null)
            {
                return StatusCode(500);
            }
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
            var result = service.GetItem(id);
            if (result == null)
            {
                return StatusCode(500);
            }
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
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var result = service?.DeleteItem(id);
            if (result == null)
            {
                return StatusCode(500);
            }
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
            if (item == null)
            {
                return BadRequest();
            }

            var result = service.AddItem(item);
            if (!result)
            {
                return StatusCode(500);
            }
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
            if (item == null)
            {
                return BadRequest();
            }

            var result = service.UpdateItem(item);
            if (!result)
            {
                return StatusCode(500);
            }
            return Ok(result);
        }
    }
}
