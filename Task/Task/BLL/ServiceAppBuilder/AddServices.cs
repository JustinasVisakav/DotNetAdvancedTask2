using Task.BLL.Interfaces;
using Task1.BLL.Services;

namespace Task.BLL.ServiceAppBuilder
{
    public static class AddServices
    {
        public static void AddBllServices(this IServiceCollection services)
        {
            services.AddScoped<ICartService, CartService>();
        }
    }
}
