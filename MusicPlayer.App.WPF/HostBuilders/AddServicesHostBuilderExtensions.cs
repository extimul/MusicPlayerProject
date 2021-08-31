using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicPlayer.App.WPF.ViewModels.Factories;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Services.Content;
using MusicPlayer.Core.Services.Audio;
using MusicPlayer.Core.Services.Dialog;
using MusicPlayer.Core.Services.Icon;
using MusicPlayer.Core.Services.Navigators;

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

                services.AddTransient(typeof(IContentContainer<>), typeof(ContentContainer<>));
                services.AddTransient(typeof(IContentManager<Playlist, Library>), typeof(LibraryManager));
                services.AddTransient(typeof(IContentManager<Track, Queue>), typeof(QueueManager));
                services.AddTransient(typeof(IContentManager<Track, Playlist>), typeof(TracksManager));
            });

            return host;
        }
    }
}
