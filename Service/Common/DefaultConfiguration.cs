
using Microsoft.Extensions.Configuration;

namespace Application
{
    public static class DefaultConfiguration
    {
        public static IConfiguration StaticConfiguration { get; set; }

        public static void SetStaticConfiguration(this IConfiguration configuration)
        {
            StaticConfiguration = configuration;
        }

        public static string GetConnectionString { get; private set; }  

        public static void setConnectionString(string connectionString)
        {
            if(string.IsNullOrWhiteSpace(connectionString))
            {
            GetConnectionString = connectionString;
            }
        }
    }
}
