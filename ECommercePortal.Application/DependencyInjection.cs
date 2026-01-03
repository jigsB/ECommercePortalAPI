using ECommercePortal.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommercePortal.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<UserService>();   // 🔴 REQUIRED
            return services;
        }
    }
}
