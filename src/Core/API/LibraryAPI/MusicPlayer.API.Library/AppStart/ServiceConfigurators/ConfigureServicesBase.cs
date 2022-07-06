using System.Reflection;
using MusicPlayer.API.Library.Application;
using MusicPlayer.API.Library.Persistence;
using ServerApp.Base.Extensions;
using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Configuration.Setup;
using ServerApp.WebApp.Base.Mappings;

namespace MusicPlayer.API.Library.AppStart.ServiceConfigurators;

[ConfigurationLoadPriority(1)]
public class ConfigureServicesBase : IAppSevicesConfigurator
{
    public void ConfigureServices(IServiceCollection services, params object[]? additionalServices)
    {
        if (additionalServices == null || !additionalServices.Any()) return;
        
        var configuration = additionalServices.GetConfigureParam<ConfigurationManager>();
            
        services.AddPersistence();
        services.AddApplication();

        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(ApplicationDependencyContainer).Assembly));
        });
            
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddMemoryCache();
        services.AddRouting();
        services.AddOptions();
        services.AddHttpContextAccessor();
        services.AddResponseCaching();
    }
}