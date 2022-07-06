using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ServerApp.WebApp.Base.Extensions;

public static class BaseServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger<T>(this IServiceCollection services)
        where T : class, IConfigureOptions<SwaggerGenOptions>
    {
        var xmlFile = $"{Assembly.GetCallingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, T>();
        services.AddSwaggerGen(config =>
        {
            config.IncludeXmlComments(xmlPath);
        });

        return services;
    }
    
    public static void AddVersioning(this IServiceCollection services, ApiVersion? apiVersion = null)
    {
        var defaultApiVersion = apiVersion ?? new ApiVersion(1, 0);
        
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