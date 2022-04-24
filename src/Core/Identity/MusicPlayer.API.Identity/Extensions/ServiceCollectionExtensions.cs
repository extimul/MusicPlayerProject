using System.Reflection;
using Microsoft.Extensions.Options;
using MusicPlayer.API.Identity.Configuration;
using MusicPlayer.API.Identity.Domain.Entities;
using MusicPlayer.API.Identity.Persistence;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MusicPlayer.API.Identity.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddIdentityService(configuration);
        
        return services;
    }
    
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
            ConfigureSwaggerOptions>();
            
        services.AddSwaggerGen(config =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            config.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}