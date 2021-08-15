using System.IO;

namespace MusicPlayer.App.WPF.Services.DataPath
{
    public class DataPathService : IDataPathService
    {
        public string DefaultTrackImage { get; set; }
        public string ApplicationDirectoryPath { get; set; }
        public string MusicContainerPath { get; set; }

        public DataPathService()
        {
            ApplicationDirectoryPath = Directory.GetCurrentDirectory();
            DefaultTrackImage = GetDefaultImagePath();
            MusicContainerPath = GetMusicContainerPath();
        }

        /// <summary>
        /// Get or create music container path
        /// </summary>
        /// <returns>path</returns>
        private string GetMusicContainerPath()
        {
            string musicContainerPath = Path.Combine(ApplicationDirectoryPath, "Musics");
            if (Directory.Exists(musicContainerPath))
                return musicContainerPath;
            else
                return Directory.CreateDirectory(musicContainerPath).FullName;
        }

        /// <summary>
        /// Get default image path for music if music doesnt contain its own image
        /// </summary>
        /// <returns>image path</returns>
        private string GetDefaultImagePath()
        {
            string imagePath = Path.Combine(ApplicationDirectoryPath, "ApplicationResources\\DefaultSongImg.png");

            if (File.Exists(imagePath)) return imagePath;
            else return string.Empty;
        }
    }
}
