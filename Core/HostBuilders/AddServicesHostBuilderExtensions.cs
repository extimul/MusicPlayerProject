using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.Core.Managers.Navigators;
using MusicPlayerProject.Core.Managers.Dialog;
using MusicPlayerProject.Core.Managers.Icon;
using MusicPlayerProject.ViewModels.Factories;


namespace MusicPlayerProject.Core.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IAudioManager, AudioManager>();
                services.AddSingleton<IPlaylistManager, PlaylistManager>();
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                services.AddSingleton<IDialogService, DialogService>();
                services.AddSingleton<IIconManager, IconManager>();
            });

            return host;
        }
    }
}
