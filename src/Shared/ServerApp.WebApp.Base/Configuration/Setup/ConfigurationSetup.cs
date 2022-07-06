using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServerApp.Base.Extensions;
using ServerApp.WebApp.Base.Common.Attributes;

namespace ServerApp.WebApp.Base.Configuration.Setup;

public static class ConfigurationSetup
{
    public static void ConfigureServicesFromAssemblies(this IServiceCollection services, IEnumerable<Assembly?> assemblies)
    {
        foreach (var assembly in assemblies)
            services.ConfigureServicesFromAssembly(assembly);
    }
    
    public static void ConfigureFromAssemblies(this IApplicationBuilder app, IEnumerable<Assembly?> assemblies)
    {
        foreach (var assembly in assemblies) 
            app.ConfigureFromAssembly(assembly);
    }
    
    public static void ConfigureServicesFromAssembly(this IServiceCollection services, Assembly? assembly = null, 
        params object[]? additionalServices)
    {
        assembly ??= Assembly.GetCallingAssembly();
        
        BaseConfigure<IAppSevicesConfigurator, IServiceCollection>(assembly, (configuration) =>
        {
            configuration?.ConfigureServices(services, additionalServices);
        });
    }
    
    public static void ConfigureFromAssembly(this IApplicationBuilder app, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();
        
        BaseConfigure<IAppConfigurator, IApplicationBuilder>(assembly, (configuration) =>
        {
            configuration?.Configure(app);
        });
    }

    private static void BaseConfigure<TInterface, TInstance>(Assembly? assembly, 
        Action<TInterface> action)
    {
        var interfaceType = typeof(TInterface);
        var types = assembly.GetTypes()
            .Where(interfaceType.IsAssignableFrom)
            .OrderBy(x => x.GetAttributeValue<ConfigurationLoadPriorityAttribute, int>(selector 
                => selector.Priority))
            .ToArray();
        
        foreach (var type in types)
        {
            var configuration = (TInterface)Activator.CreateInstance(type)!;
            action?.Invoke(configuration);
        }
    }
}