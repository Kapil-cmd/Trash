
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Injection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            return services;
        }
    }
}
