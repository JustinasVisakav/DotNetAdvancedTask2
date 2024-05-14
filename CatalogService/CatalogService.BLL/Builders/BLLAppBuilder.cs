using CatalogService.BLL.Interfaces;
using CatalogService.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Builders
{
    public static class BLLAppBuilder
    {
        public static void AddBllServices(this IServiceCollection service)
        {
            service.AddScoped<ICategoryService,CategoryService>();
            service.AddScoped<IItemService,ItemService>();
        }
    }
}
