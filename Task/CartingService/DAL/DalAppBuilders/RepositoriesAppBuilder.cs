using System.Runtime.CompilerServices;
using CartingService.BLL.Models;
using CartingService.DAL.Interfaces;
using CartingService.DAL.Repositories;

namespace CartingService.DAL.DalAppBuilders
{
    public static class RepositoriesAppBuilder
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<CartModel>, CartRepository>();
        }
    }
}
