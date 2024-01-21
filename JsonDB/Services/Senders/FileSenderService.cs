using Telegram.Bot;

namespace JsonDB.Services.Senders
{
    public static class FileSenderService
    {
        private static readonly string botToken = "6851196120:AAHW799nQvRys-b9LxYJWAHpNnCa4TbI4HQ";
        private static readonly TelegramBotClient botClient = new TelegramBotClient(botToken);

        public static async Task SendFile(string zipFilePath, long chatId = 5617428170)
        {
            if (File.Exists(zipFilePath))
            {
                using (Stream fileStream = new FileStream(zipFilePath, FileMode.Open, FileAccess.Read))
                {
                    await botClient.SendDocumentAsync(
                        chatId: 5617428170,
                        document: Telegram.Bot.Types.InputFile.FromStream(
                            stream: fileStream,
                            fileName: Path.GetFileName(zipFilePath)));
                }

                //Console.WriteLine($"File sent successfully.{zipFilePath}");
            }
        }
    }
}
