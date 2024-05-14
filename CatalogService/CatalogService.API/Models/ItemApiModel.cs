using CatalogService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Models
{
    public class ItemApiModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string LinkToCategory { get; set; }
        public CategoryDtoModel? Category { get; set; }
    }

    public static class ItemApiModelExtensions
    {
        public static ItemApiModel ToApiModel(this ItemDtoModel dto)
        {
            ItemApiModel api = new ItemApiModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Image = dto.Image,
                Price = dto.Price,
                Amount = dto.Amount,
                Category = dto.Category                
            };

            if (dto.Category != null)
                api.LinkToCategory = $"category/{dto.Category.Id}";

            return api;
        }
    }
}
