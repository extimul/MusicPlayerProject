using MusicPlayer.Core.Types;
using System.Collections.Generic;
using System.IO;

namespace MusicPlayer.Core.Services.Content
{
    public sealed class DataPathService : IDataPathService
    {
        #region Properties
        //folders
        public string ApplicationDirectoryPath { get; set; }
        public string MusicDirectoryPath { get; set; }
        public string ApplicationDataDirectoryPath { get; set; }
        public string PlaylistsDirectoryPath { get; set; }
        public string ImagesDirectoryPath { get; set; }

        //files
        public string QueueJsonPath { get; set; }
        public string DefaultTrackImagePath { get; set; }
        #endregion

        public DataPathService()
        {
            // cd
            ApplicationDirectoryPath = Directory.GetCurrentDirectory(); // get current directory path

            // cd/data
            ApplicationDataDirectoryPath = InitDirectory(DirectoryNames.DATA); // cd/data
            PlaylistsDirectoryPath = InitDirectory(DirectoryNames.PLAYLISTS);
            ImagesDirectoryPath = InitDirectory(DirectoryNames.IMAGES);
            
            // cd/musics
            MusicDirectoryPath = InitDirectory(DirectoryNames.MUSICS); // cd/musics

            // files
            QueueJsonPath = CreateJsonFile(ApplicationDataDirectoryPath + "\\queue", FileNames.QUEUE); // create json file at cd//data/queue
            DefaultTrackImagePath = GetDefaultImagePath(FileNames.DEFAULT_IMG);
        }

        public string GeneratePlaylistJsonFileName(string fileName)
        {
            return Path.Combine(PlaylistsDirectoryPath, "playlist_" + fileName + ".json");
        }

        public List<string> GetPlaylistFileNames(string searchPattern)
        {
            List<string> fileNamesCollection = new();
            string[] files = Directory.GetFiles(PlaylistsDirectoryPath, "*.json");

            foreach (string file in files)
            {
                if (file.Contains("playlist")) fileNamesCollection.Add(file);
            }

            return fileNamesCollection;
        }

        private string InitDirectory(string directoryName)
        {
            string directory = Path.Combine(ApplicationDirectoryPath, directoryName);
            return Directory.Exists(directory) ? directory : Directory.CreateDirectory(directory).FullName;
        }

        /// <summary>
        /// Get default image path for music if music doesnt contain its own image
        /// </summary>
        /// <returns>image path</returns>
        private string GetDefaultImagePath(string fileName)
        {
            string imagePath = Path.Combine(ApplicationDirectoryPath, DirectoryNames.RESOURCES + "\\" + fileName + ".png");

            if (File.Exists(imagePath)) return imagePath;
            else return string.Empty;
        }

        private string CreateJsonFile(string basePath, string filename)
        {
            string playlistJsonFile = Path.Combine(basePath, filename + ".json");

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
