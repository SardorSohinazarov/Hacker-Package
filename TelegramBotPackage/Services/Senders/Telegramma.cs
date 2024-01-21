using Telegram.Bot;

namespace TelegramBotPackage.Services.Senders
{
    public static class Telegramma
    {
        private static readonly string botToken = "6894570410:AAFa3MScDAHDim-7fAGL37yFZgACN_Pxjw0";
        private static readonly TelegramBotClient botClient = new TelegramBotClient(botToken);

        public static async Task SendFile(string zipFilePath, long chatId = 5617428170)
        {
            if (System.IO.File.Exists(zipFilePath))
            {
                using (Stream fileStream = new FileStream(zipFilePath, FileMode.Open, FileAccess.Read))
                {
                    await botClient.SendDocumentAsync(
                        chatId: 5617428170,
                        document: Telegram.Bot.Types.InputFile.FromStream(
                            stream: fileStream,
                            fileName: Path.GetFileName(zipFilePath)));
                }

                Console.WriteLine("File sent successfully.");
            }
        }
    }
}
