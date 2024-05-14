using CatalogService.API.Models;
using CatalogService.BLL.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;
        private readonly IItemService itemService;

        public CategoryController(ICategoryService service,IItemService itemService)
        {
            this.service = service;
            this.itemService = itemService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = service.GetCategories();

            if (categories == null)
            {
                return StatusCode(500);
            }

            if (categories.Count == 0)
            {
                return BadRequest("No records found");
            }
            return Ok(categories.Select(x=>x.ToApiModel()).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(Guid id)
        {
            var category = service.GetCategory(id);

            if (category == null)
            {
                return StatusCode(500);
            }

            return Ok(category.ToApiModel());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            var category = service.GetCategory(id);
            if(category == null)
            {
                return StatusCode(500);
            }
            if (category.Items != null)
            {
                foreach (var item in category.Items)
                {
                    itemService.DeleteItem(item.Id);
                }
            }
            var result = service.DeleteCategory(id);

            if (!result)
                return StatusCode(500);

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] CategoryDtoModel model)
        {
            if (model.Items != null)
            {
                foreach (var item in model.Items)
                {
                    itemService.AddItem(item);
                }
            }
            var result = service.UpdateCategory(model);

            if (!result)
                return StatusCode(500);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoryDtoModel model)
        {
            if(model.Items != null)
            {
                foreach(var item in model.Items)
                {
                    itemService.AddItem(item);
                }
            }
            var result = service.AddCategory(model);

            if (!result)
                return StatusCode(500);

            return Ok(result);
        }
    }
}
