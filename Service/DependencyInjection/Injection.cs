
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Injection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<UserService>();
            services.AddScoped<ItemTypeService>();
            services.AddScoped<ItemService>();
            services.AddScoped<ListService>();
            services.AddScoped<UserRoleService>();


            return services;
        }
    }
}
