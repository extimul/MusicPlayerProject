using EasyServiceConfigurator.AspNet;
using ServerApp.WebApp.Base.Common.Attributes;

namespace MusicPlayer.IdentityService.AppStart.Configurators;

[ConfigurationLoadPriority(4)]
public class ConfigureEndpoints : IAppConfigurator
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });
    }
}