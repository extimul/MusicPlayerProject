using EasyServiceConfigurator;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.App.WPF.Services.NavigationService;

namespace MusicPlayer.App.WPF.AppConfiguration;

[ServiceLoadPriority(2)]
public sealed class ConfigureAppServices : IServicesConfigurator
{
    public void ConfigureServices(IServiceCollection services, params object[]? additionalServices)
    {
        services.AddSingleton<INavigationService, NavigationService>();
    }
}