using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.Core.Managers.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            });

            return host;
        }
    }
}
