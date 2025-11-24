
using Application;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<DbConnectionFactory>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IItemTypeRepository,ItemTypeRepository>();
            services.AddScoped<IItemRepository,ItemRepository>();
            services.AddScoped<IListRepository,ListRepository>();
            services.AddScoped<IRolePermissionRepository,RolePermissionRepository>();
            return services;
        }
    }
}
