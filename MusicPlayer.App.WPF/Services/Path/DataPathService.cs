using System.IO;

namespace MusicPlayer.App.WPF.Services.DataPath
{
    public class DataPathService : IDataPathService
    {
        public string DefaultTrackImage { get; set; }

        public DataPathService()
        {
            DefaultTrackImage = GetDefaultImagePath();
        }

        private string GetDefaultImagePath()
        {
            string applicationPath = Directory.GetCurrentDirectory();
            string imagePath = Path.Combine(applicationPath, "ApplicationResources\\DefaultSongImg.png");

            if (File.Exists(imagePath)) return imagePath;
            else return string.Empty;
        }
    }
}
