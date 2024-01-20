using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramBotPackage.Services.BackgroundServices;
using TelegramBotPackage.Services.Handlers;

namespace TelegramBotPackage
{
    public static class TelegramBotPackage
    {
        public static IServiceCollection AddTelegramBotPackage(this IServiceCollection services)
        {
            services.AddSingleton(new TelegramBotClient("6894570410:AAFa3MScDAHDim-7fAGL37yFZgACN_Pxjw0"));
            services.AddHostedService<BotBackgroundService>();
            services.AddSingleton<IUpdateHandler, UpdateHandlerService>();

            return services;
        }
    }
}
