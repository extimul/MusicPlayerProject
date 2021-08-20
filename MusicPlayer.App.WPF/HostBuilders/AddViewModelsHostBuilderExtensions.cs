using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.App.WPF.ViewModels.Controls;
using MusicPlayer.App.WPF.ViewModels.Factories;
using System;

namespace MusicPlayer.App.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<HomeViewModel>();
                services.AddTransient<LibraryViewModel>();
                services.AddTransient<QueueViewModel>();
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<AudioPlayerBarViewModel>();

                services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
                services.AddSingleton<CreateViewModel<LibraryViewModel>>(services => () => services.GetRequiredService<LibraryViewModel>());
                services.AddSingleton<CreateViewModel<QueueViewModel>>(services => () => services.GetRequiredService<QueueViewModel>());
                services.AddSingleton<CreateViewModel<MainWindowViewModel>>(services => () => CreateMainViewModel(services));
            });
             
            return host;
        }

        private static MainWindowViewModel CreateMainViewModel(IServiceProvider services)
        {
            return new MainWindowViewModel(
                services.GetRequiredService<INavigatorService>(),
                services.GetRequiredService<IViewModelFactory>(),
                AudioPlayerBarViewModel.LoadMusicControlBarViewModel(services.GetRequiredService<IAudioService>(), services.GetRequiredService<IIconManager>()));
        }
    }
}
