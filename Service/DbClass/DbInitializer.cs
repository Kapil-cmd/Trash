
using Core;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DbSeed
    {
        public static void DbInitializer(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var scopeServices = scope.ServiceProvider;

            var userRepo = scopeServices.GetRequiredService<IUserRepository>();

            var list = userRepo.GetAllUsers();
            if (list.Data == null)
            {
                    var dbSeed = new DbSeedModel()
                    {
                        EmailAddress = "SuperAdmin@proton.me",
                        Password = "superadmin123",
                        UserName = "superadmin"
                    };
                    userRepo.DbSeed(dbSeed);
            }

        }
    }

}
