using EasyServiceConfigurator;
using ServerApp.WebApp.Base.Common.Attributes;

namespace MusicPlayer.IdentityService.AppStart.ConfigureServices;

[ConfigurationLoadPriority(4)]
public class ConfigureServicesControllers : IServicesConfigurator
{
    public void ConfigureServices(IServiceCollection services, params object[]? additionalServices)
    {
        services.AddControllersWithViews();
        services.AddRazorPages();
    }
}