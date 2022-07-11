using System.Reflection;
using EasyServiceConfigurator;
using Microsoft.Extensions.Options;
using MusicPlayer.API.Library.AppStart.Swagger;
using ServerApp.WebApp.Base.Common.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MusicPlayer.API.Library.AppStart.ServiceConfigurators;

[ConfigurationLoadPriority(4)]
public class ConfigureServicesSwagger : IServicesConfigurator
{
    public void ConfigureServices(IServiceCollection services, params object[]? additionalServices)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(config =>
        {
            config.IncludeXmlComments(xmlPath);
        });
    }
}