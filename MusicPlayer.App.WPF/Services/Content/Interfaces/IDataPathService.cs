using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Content
{
    public interface IDataPathService
    {
        string DefaultTrackImagePath { get; set; }
        string ApplicationDirectoryPath { get; set; }
        string MusicDirectoryPath { get; set; }
        string ApplicationDataDirectoryPath { get; set; }
        string QueueJsonPath { get; set; }
        string GeneratJsonFileName(string fileName);
        List<string> GetFileNames(string searchPattern);
    }
}