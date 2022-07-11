using EasyServiceConfigurator.AspNet;
using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Middleware;

namespace MusicPlayer.API.Library.AppStart.Configurators;

[ConfigurationLoadPriority(1)]
public class ConfigureMiddlewares : IAppConfigurator
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}