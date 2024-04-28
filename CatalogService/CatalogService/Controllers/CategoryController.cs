using CatalogService.BLL.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetCategories());
        }

        [HttpPost]
        public IActionResult Post()
        {
            Random rand= new Random();  
            CategoryModel model = new CategoryModel()
            {
                Id = Guid.NewGuid(),
                Name = "Name "+rand.Next(1,100).ToString(),
                ParentId=Guid.Parse("cee0228e-9eb0-4aea-bdd0-711a2e33def0")
                
            };
            return Ok(service.AddCategory(model));
        }
    }
}
