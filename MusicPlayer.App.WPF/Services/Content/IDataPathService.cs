namespace MusicPlayer.App.WPF.Services.Content
{
    public interface IDataPathService
    {
        string DefaultTrackImagePath { get; set; }
        string ApplicationDirectoryPath { get; set; }
        string MusicDirectoryPath { get; set; }
        string ApplicationDataDirectoryPath { get; set; }
        string PlaylistJsonPath { get; set; }
        string QueueJsonPath { get; set; }
    }
}