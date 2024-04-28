using CartingService.BLL.Interfaces;
using CartingService.BLL.Services;

namespace CartingService.BLL.ServiceAppBuilder
{
    public static class AddServices
    {
        public static void AddBllServices(this IServiceCollection services)
        {
            services.AddScoped<ICartService, CartService>();
        }
    }
}
