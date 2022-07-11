using EasyServiceConfigurator.AspNet;
using ServerApp.WebApp.Base.Common.Attributes;

namespace MusicPlayer.IdentityService.AppStart.Configurators;

[ConfigurationLoadPriority(3)]
public class ConfigureAuthentication : IAppConfigurator
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseIdentityServer();
    }
}