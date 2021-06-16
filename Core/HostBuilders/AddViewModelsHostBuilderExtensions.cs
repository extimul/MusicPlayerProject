using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.Core.Managers.Navigators;
using MusicPlayerProject.ViewModels;
using MusicPlayerProject.ViewModels.Base;
using MusicPlayerProject.ViewModels.Factories;
using System;

namespace MusicPlayerProject.Core.HostBuilders
{
    public static class AddViewModelsHostBuilderExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<HomeViewModel>();
                services.AddTransient<LibraryViewModel>();
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<AudioPlayerBarViewModel>();

                services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
                services.AddSingleton<CreateViewModel<LibraryViewModel>>(services => () => services.GetRequiredService<LibraryViewModel>());
                services.AddSingleton<CreateViewModel<MainWindowViewModel>>(services => () => CreateMainViewModel(services));

                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            });
             
            return host;
        }

        private static MainWindowViewModel CreateMainViewModel(IServiceProvider services)
        {
            return new MainWindowViewModel(
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IViewModelFactory>(),
                AudioPlayerBarViewModel.LoadMusicControlBarViewModel(services.GetRequiredService<IAudioManager>()));
        }
    }
}
