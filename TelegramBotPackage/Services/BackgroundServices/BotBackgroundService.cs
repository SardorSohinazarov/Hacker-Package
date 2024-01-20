using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TelegramBotPackage.Services.BackgroundServices
{
    public class BotBackgroundService : BackgroundService
    {
        private readonly TelegramBotClient _client;
        private readonly ILogger<BotBackgroundService> _logger;
        private readonly IUpdateHandler _updateHandler;

        public BotBackgroundService(
            TelegramBotClient telegramBotClient,
            ILogger<BotBackgroundService> logger,
            IUpdateHandler updateHandler)
        {
            _logger = logger;
            _client = telegramBotClient;
            _updateHandler = updateHandler;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var bot = await _client.GetMeAsync(stoppingToken);
            _logger.LogInformation("Bot started succesfully: @{bot.Username}", bot.Username);

            _client.StartReceiving(
                _updateHandler.HandleUpdateAsync,
                _updateHandler.HandlePollingErrorAsync,
                new ReceiverOptions() { ThrowPendingUpdates = true },
                stoppingToken);
        }
    }
}
