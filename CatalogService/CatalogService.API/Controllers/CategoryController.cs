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

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryApiModel>), 200)]
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

        /// <summary>
        /// Gets specific category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryApiModel), 200)]
        public IActionResult GetCategory(Guid id)
        {
            var category = service.GetCategory(id);

            if (category == null)
            {
                return StatusCode(500);
            }

            return Ok(category.ToApiModel());
        }

        /// <summary>
        /// Deletes category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns boolean of opperation success</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
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

        /// <summary>
        /// Modifies category
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Boolean of category success</returns>
        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
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

        /// <summary>
        /// Creates new category
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Boolean of operation success</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
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
