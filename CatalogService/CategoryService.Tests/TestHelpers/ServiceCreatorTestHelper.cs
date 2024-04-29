using CatalogService.BLL.Builders;
using CatalogService.DAL.Builders;
using CatalogService.DAL.ContextKeeper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
