using MusicPlayer.Core.Types;
using System.IO;

namespace MusicPlayer.App.WPF.Services.Content
{
    public sealed class DataPathService : IDataPathService
    {
        #region Properties
        public string DefaultTrackImagePath { get; set; }
        public string ApplicationDirectoryPath { get; set; }
        public string MusicDirectoryPath { get; set; }
        public string ApplicationDataDirectoryPath { get; set; }
        public string PlaylistJsonPath { get; set; }
        public string QueueJsonPath { get; set; }
        #endregion

        public DataPathService()
        {
            ApplicationDirectoryPath = Directory.GetCurrentDirectory();
            MusicDirectoryPath = GetContainerPath(DirectoryNames.MUSICS);
            ApplicationDataDirectoryPath = GetContainerPath(DirectoryNames.DATA);
            PlaylistJsonPath = GetJsonFilePath(FileNames.PLAYLIST);
            QueueJsonPath = GetJsonFilePath(FileNames.QUEUE);
            DefaultTrackImagePath = GetDefaultImagePath();
        }

        private string GetContainerPath(string directoryName)
        {
            string directory = Path.Combine(ApplicationDirectoryPath, directoryName);
            return Directory.Exists(directory) ? directory : Directory.CreateDirectory(directory).FullName;
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

        private string GetJsonFilePath(string filename)
        {
            string playlistJsonFile = Path.Combine(ApplicationDataDirectoryPath, $"{filename}.json");

            if (File.Exists(playlistJsonFile))
            {
                return playlistJsonFile;
            }
            else
            {
                File.Create(playlistJsonFile);
                return playlistJsonFile;
            }
        }
    }
}
