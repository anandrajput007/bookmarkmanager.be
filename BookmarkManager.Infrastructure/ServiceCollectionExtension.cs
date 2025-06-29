using BookmarkManager.Domain.Interfaces;
using BookmarkManager.Infrastructure.Database;
using BookmarkManager.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookmarkManager.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IBookmarkRepository, BookmarkRepository>();
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
} 