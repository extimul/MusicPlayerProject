using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.Services.Dialog;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.ViewModels.Factories;
using MusicPlayer.App.WPF.Services.Settings;
using MusicPlayer.App.WPF.Services.Content;

namespace MusicPlayer.App.WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigatorService, NavigatorService>();
                services.AddSingleton<IAudioService, AudioService>();

                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                services.AddSingleton<IDialogService, DialogService>();
                services.AddSingleton<IIconManager, IconManager>();
                services.AddSingleton<IDataPathService, DataPathService>();
                services.AddSingleton<IApplicationSettingsService, ApplicationSettingsService>();

                services.AddScoped(typeof(ITracksCollectionService<>), typeof(TracksCollectionService<>));
                services.AddScoped(typeof(IContentHandler<>), typeof(ContentHandler<>));
            });

            return host;
        }
    }
}
