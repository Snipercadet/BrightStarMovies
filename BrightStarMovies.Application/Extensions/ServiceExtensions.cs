using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BrightStarMovies.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
