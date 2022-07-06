using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Configuration.Setup;
using ServerApp.WebApp.Base.Middleware;

namespace MusicPlayer.IdentityService.AppStart.Configurators;

[ConfigurationLoadPriority(2)]
public class ConfigureMiddlewares : IAppConfigurator
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}