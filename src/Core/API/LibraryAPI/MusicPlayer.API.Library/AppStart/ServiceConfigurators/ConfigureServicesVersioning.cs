using ServerApp.Base;
using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Configuration.Setup;

namespace MusicPlayer.API.Library.AppStart.ServiceConfigurators;

[ConfigurationLoadPriority(3)]
public class ConfigureServicesVersioning : IAppSevicesConfigurator
{
    public void ConfigureServices(IServiceCollection services, params object[]? additionalServices)
    {
        var defaultApiVersion = ServerAppBase.ApiVersion;
        
        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = defaultApiVersion;
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        });
            
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
    }
}