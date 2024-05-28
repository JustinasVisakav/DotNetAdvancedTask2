using CatalogSercice.RabbitMq.Interfaces;
using CatalogService.API.Models;
using CatalogService.RabbitMq.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RabController : ControllerBase
    {
        private readonly IRabbitMq rabbitMq;
        private readonly FailedMessageStorage stor;

        public RabController(IRabbitMq rabbitMq, FailedMessageStorage stor)
        {
            this.rabbitMq = rabbitMq;
            this.stor = stor;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ItemApiModel>), 200)]
        public IActionResult Get([FromQuery] Guid categoryId)
        {
            stor.Messages.Enqueue("one");
            stor.Messages.Enqueue("two");
            rabbitMq.CheckFailedMessageQueue();
            return Ok();
        }
    }
}
