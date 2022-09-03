using CardStorage.Data;
using CardStorage.Data.Requests.AuthRequests;
using CardStorage.Services.AuthService;
using Microsoft.EntityFrameworkCore;

namespace CardStorage.Utils
{
    static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseContext(
            this IServiceCollection services, IConfiguration config, bool isDevelopment)
        {
            var options = config.GetSection(MySQLOptions.Position).Get<MySQLOptions>();
            var connectionString = options.GetConnectionString();
            var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));

            services.AddDbContext<DatabaseContext>(
                dbContextOptions =>
                {
                    dbContextOptions.UseMySql(connectionString, serverVersion)
                        .EnableDetailedErrors();

                    if (isDevelopment)
                    {
                        dbContextOptions.LogTo(Console.WriteLine, LogLevel.Information)
                            .EnableSensitiveDataLogging();
                    }
                    else
                    {
                        dbContextOptions.LogTo(Console.WriteLine, LogLevel.Warning)
                            .EnableSensitiveDataLogging();
                    }
                });

            return services;
        }

        public static IServiceCollection AddAuthService(
            this IServiceCollection services)
        {
            services.AddSingleton<IAuthService, AuthService>();

            return services;
        }
    }
}
