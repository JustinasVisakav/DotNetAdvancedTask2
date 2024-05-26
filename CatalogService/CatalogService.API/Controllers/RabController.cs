using CatalogSercice.Infrastructure.Queue.Interfaces;
using CatalogService.API.Models;
using CatalogService.BLL.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RabController : ControllerBase
    {
        private readonly IRabbitMq rabbitMq;

        public RabController(IRabbitMq rabbitMq)
        {
            this.rabbitMq = rabbitMq;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ItemApiModel>), 200)]
        public IActionResult Get([FromQuery] Guid categoryId)
        {
            //return Ok(rabbitMq.SendMessage());
            return Ok();
        }
    }
}
