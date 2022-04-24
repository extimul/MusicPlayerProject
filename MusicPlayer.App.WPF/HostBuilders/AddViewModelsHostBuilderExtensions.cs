using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicPlayer.App.WPF.ViewModels;
using MusicPlayer.App.WPF.ViewModels.Controls;
using MusicPlayer.App.WPF.ViewModels.Factories;
using MusicPlayer.Core.MVVMBase;
using MusicPlayer.Core.Services.Audio;
using MusicPlayer.Core.Services.Icon;
using MusicPlayer.Core.Services.Navigators;
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

                services.AddScoped<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
                services.AddScoped<CreateViewModel<LibraryViewModel>>(services => () => services.GetRequiredService<LibraryViewModel>());
                services.AddScoped<CreateViewModel<QueueViewModel>>(services => () => services.GetRequiredService<QueueViewModel>());
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
