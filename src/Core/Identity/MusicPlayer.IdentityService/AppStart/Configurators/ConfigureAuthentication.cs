using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Configuration.Setup;

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