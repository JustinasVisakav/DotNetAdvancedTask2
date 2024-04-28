using CatalogService.BLL.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService service;

        public ItemController(IItemService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok(service.GetItems());
        }

        [HttpPost]
        public IActionResult PostItem()
        {
            Random random = new Random();
            ItemModel model = new ItemModel()
            {
                Id = Guid.NewGuid(),
                Amount = random.Next(1, 100),
                Description = "Desc " + random.Next(1, 100).ToString(),
                Name = "Name " + random.Next(1, 100).ToString(),
                Price = random.Next(1, 100),
                Image = "Image " + random.Next(1, 100),
                CategoryId = Guid.Parse("cee0228e-9eb0-4aea-bdd0-711a2e33def0")
            };
            return Ok(service.AddItem(model));
        }
    }
}
