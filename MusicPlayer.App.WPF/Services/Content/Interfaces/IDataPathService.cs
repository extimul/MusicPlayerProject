using System.Collections.Generic;

namespace MusicPlayer.App.WPF.Services.Content
{
    public interface IDataPathService
    {
        //Current app directory, where .exe is located. P.S Short version - CD
        string ApplicationDirectoryPath { get; set; }
        // CD/applicationResources
        string DefaultTrackImagePath { get; set; }
        // CD/musics
        string MusicDirectoryPath { get; set; }

        // CD/data/
        string ApplicationDataDirectoryPath { get; set; }

        // CD/data/playlists
        string PlaylistsDirectoryPath { get; set; }

        // CD/data/img
        string ImagesDirectoryPath { get; set; }

        // CD/data/queue/queue.json
        string QueueJsonPath { get; set; }


        string GeneratePlaylistJsonFileName(string fileName);
        List<string> GetPlaylistFileNames(string searchPattern);
    }
}