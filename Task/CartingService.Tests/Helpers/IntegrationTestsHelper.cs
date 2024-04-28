using Microsoft.Extensions.DependencyInjection;
using CartingService.BLL.Interfaces;
using CartingService.BLL.Services;
using LiteDB;
using CartingService.BLL.Models;
using CartingService.DAL.Interfaces;
using CartingService.DAL.Repositories;

namespace CartingService.Tests.Helpers
{
    public static class IntegrationTestsHelper
    {
        public static IServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICartService, CartService>();
            serviceCollection.AddSingleton<LiteDatabase>(x => new LiteDatabase("@Test.Database"));
            serviceCollection.AddScoped<IGenericRepository<CartModel>, CartRepository>();


            return serviceCollection.BuildServiceProvider();
        }
    }
}
