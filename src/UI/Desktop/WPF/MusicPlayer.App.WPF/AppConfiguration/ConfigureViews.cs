using EasyServiceConfigurator;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.App.WPF.Views.Windows;

namespace MusicPlayer.App.WPF.AppConfiguration;

[ServiceLoadPriority(1)]
public class ConfigureViews : IServicesConfigurator
{
    public void ConfigureServices(IServiceCollection services, params object[]? additionalServices)
    {
        services.AddSingleton<MainView>();
    }
}