using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookmarkManager.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Removed AutoMapper registration

            return services;
        }
    }
} 