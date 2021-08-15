namespace MusicPlayer.App.WPF.Services.DataPath
{
    public interface IDataPathService
    {
        string DefaultTrackImage { get; set; }
        string ApplicationDirectoryPath { get; set; }
        string MusicContainerPath { get; set; }
    }
}