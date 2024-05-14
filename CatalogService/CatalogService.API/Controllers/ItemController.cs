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

        [HttpGet]
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

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = service.GetItem(id);
            if (result == null)
            {
                return StatusCode(500);
            }
            return Ok(result.ToApiModel());
        }

        [HttpDelete("{id}")]
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

        [HttpPost]
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

        [HttpPut]
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
