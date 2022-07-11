using EasyServiceConfigurator;
using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Middleware;

namespace MusicPlayer.IdentityService.AppStart.ConfigureServices;

[ConfigurationLoadPriority(3)]
public class ConfigureServicesMiddleware : IServicesConfigurator
{
    public void ConfigureServices(IServiceCollection services, params object[]? additionalServices)
    {
        services.AddScoped<ExceptionHandlingMiddleware>();
    }
}