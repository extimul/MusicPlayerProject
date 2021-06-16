using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.Core.Managers.Navigators;
using MusicPlayerProject.ViewModels;
using MusicPlayerProject.ViewModels.Factories;
using MusicPlayerProject.Views.Windows;
using System;

namespace MusicPlayerProject.Core.HostBuilders
{
    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>()));
            });

            return host;
        }

        
    }
}
