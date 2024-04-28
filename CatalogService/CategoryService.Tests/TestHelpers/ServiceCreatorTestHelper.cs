using CatalogService.BLL.Builders;
using CatalogService.BLL.Interfaces;
using CatalogService.BLL.Services;
using CatalogService.DAL.Builders;
using CatalogService.DAL.Repositories;
using CatalogService.Domain.ContextKeeper;
using CatalogService.Domain.Interfaces;
using CategoryService.Tests.DalTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CategoryService.Tests.TestHelpers
{
    public static class ServiceCreatorTestHelper
    {
        public static IServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBllServices();
            serviceCollection.AddDalServices();
            serviceCollection.AddDbContext<DatabaseContext>(context => context.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            var inMemorySettings = new Dictionary<string, string> { { "IsUnitTest", "true" } };

            return serviceCollection.BuildServiceProvider();
        }
    }
}
