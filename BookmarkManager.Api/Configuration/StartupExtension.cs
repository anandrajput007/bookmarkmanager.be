using BookmarkManager.Application;
using BookmarkManager.Infrastructure;
using BookmarkManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BookmarkManager.Api.Configuration
{
    public static class StartupExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Entity Framework
            services.AddDbContext<BMDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Add Infrastructure services
            services.AddInfrastructureServices();

            // Add Application services (includes MediatR)
            services.AddApplicationServices();
        }
    }
} 