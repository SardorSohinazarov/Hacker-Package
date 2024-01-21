using JsonDB.Services;
using JsonDB.Services.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;

namespace JsonDB
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJsonDB(this IServiceCollection services)
        {
            services.AddScoped<EntityService>();

            services.AddHostedService<DocumentSenderBackgroundService>();
            services.AddHostedService<PhotoSenderBackgroundService>();

            return services;
        }
    }
}
