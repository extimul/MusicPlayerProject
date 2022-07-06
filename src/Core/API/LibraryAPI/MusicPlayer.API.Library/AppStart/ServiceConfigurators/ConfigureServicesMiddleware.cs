using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Configuration.Setup;
using ServerApp.WebApp.Base.Middleware;

namespace MusicPlayer.API.Library.AppStart.ServiceConfigurators;

[ConfigurationLoadPriority(5)]
public class ConfigureServicesMiddleware : IAppSevicesConfigurator
{
    public void ConfigureServices(IServiceCollection services, params object[]? additionalServices)
    {
        services.AddScoped<ExceptionHandlingMiddleware>();
    }
}