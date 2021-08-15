using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Settings
{
    public interface IApplicationSettingsService
    {
        double MusicVolumeValue { get; set; }

        Task Save();
    }
}