using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Configuration.Setup;

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