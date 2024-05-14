using CatalogService.DAL.Extensions;
using CatalogService.DAL.Interfaces;
using CatalogService.DAL.Repositories;
using CatalogService.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.Builders
{
    public static class DalAppBuilder
    {
        public static void AddDalServices(this IServiceCollection service)
        {
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IItemRepository, ItemRepository>();
            service.AddScoped<IItemMapper,ItemMapper>();
            service.AddScoped<ICategoryMapper, CategoryMapper>();
        }
    }
}
