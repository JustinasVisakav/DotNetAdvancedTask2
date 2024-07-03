using CatalogService.API.Models;
using CatalogService.BLL.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;
        private readonly IItemService itemService;
        private readonly ILogger<CategoryController> logger;
        private const string controllerName = "CategoryController";

        public CategoryController(ICategoryService service, IItemService itemService, ILogger<CategoryController> logger)
        {
            this.service = service;
            this.itemService = itemService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryApiModel>), 200)]
        public IActionResult Get()
        {
            logger.LogInformation($"Location: {controllerName}, request Get categories");
            var categories = service.GetCategories();

            if (categories == null)
            {
                logger.LogInformation($"Location: {controllerName}, request failed");
                return StatusCode(500);
            }

            if (categories.Count == 0)
            {
                logger.LogInformation($"Location: {controllerName}, bad request");
                return BadRequest("No records found");
            }

            logger.LogInformation($"Location: {controllerName}, request success");
            return Ok(categories.Select(x => x.ToApiModel()).ToList());
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
            logger.LogInformation($"Location: {controllerName}, request {id} for get");
            var category = service.GetCategory(id);

            if (category == null)
            {
                logger.LogInformation($"Location: {controllerName}, request {id} failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request {id} success");
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
            logger.LogInformation($"Location: {controllerName}, request {id} for delete");

            var category = service.GetCategory(id);
            if (category == null)
            {
                logger.LogInformation($"Location: {controllerName}, request {id} Failed");
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
            {
                logger.LogInformation($"Location: {controllerName}, request {id} Failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request {id} success");
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
            logger.LogInformation($"Location: {controllerName}, request {model.Id} for modify");
            if (model.Items != null)
            {
                foreach (var item in model.Items)
                {
                    itemService.AddItem(item);
                }
            }
            var result = service.UpdateCategory(model);

            if (!result)
            {
                logger.LogInformation($"Location: {controllerName}, request {model.Id} Failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request {model.Id} Success");
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
            logger.LogInformation($"Location: {controllerName}, request {model.Id} for insert");
            if (model.Items != null)
            {
                foreach (var item in model.Items)
                {
                    itemService.AddItem(item);
                }
            }
            var result = service.AddCategory(model);

            if (!result)
            {
                logger.LogInformation($"Location: {controllerName}, request {model.Id} Failed");
                return StatusCode(500);
            }

            logger.LogInformation($"Location: {controllerName}, request {model.Id} Success");
            return Ok(result);
        }
    }
}
