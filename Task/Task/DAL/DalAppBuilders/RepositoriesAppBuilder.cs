using System.Runtime.CompilerServices;
using Task.BLL.Models;
using Task.DAL.Interfaces;
using Task.DAL.Repositories;

namespace Task.DAL.DalAppBuilders
{
    public static class RepositoriesAppBuilder
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<CartModel>, CartRepository>();
        }
    }
}
