using CatalogService.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.JSInterop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Models
{
    public class CategoryApiModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public CategoryDtoModel? Parent { get; set; }
        public List<ItemDtoModel>? Items { get; set; }
        public string ItemListByCategory { get; set; }
    }

    public static class CategoryApiModelExtension
    {
        public static CategoryApiModel ToApiModel(this CategoryDtoModel dto)
        {
            var api = new CategoryApiModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Image = dto.Image,
                Parent = dto.Parent,
                Items = dto.Items,
                ItemListByCategory = $"item?categoryid={dto.Id}"
            };
            return api;
        }
    }
}
