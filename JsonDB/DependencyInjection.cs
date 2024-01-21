using Microsoft.Extensions.DependencyInjection;

namespace JsonDB
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJsonDBPackage(this IServiceCollection services)
        {
            services.AddScoped<EntityService>();

            return services;
        }
    }
}
