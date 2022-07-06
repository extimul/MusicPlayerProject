using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.IdentityService.Application;
using MusicPlayer.IdentityService.Application.Interfaces;
using MusicPlayer.IdentityService.Domain.Entities;
using MusicPlayer.IdentityService.Persistence;
using ServerApp.Base.Extensions;
using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Configuration.Setup;
using ServerApp.WebApp.Base.Mappings;

namespace MusicPlayer.IdentityService.AppStart.ConfigureServices;

[ConfigurationLoadPriority(1)]
public class ConfigureServicesBase : IAppSevicesConfigurator
{
    public void ConfigureServices(IServiceCollection services, params object[]? additionalServices)
    {
        var configuration = additionalServices.GetConfigureParam<ConfigurationManager>();
        
        services.AddPersistence(configuration);
        services.AddApplication();
        
        services.AddIdentity<ApplicationUser, IdentityRole<long>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        services.Configure<IdentityOptions>(configuration.GetSection("IdentityServerOptions"));
        
        services.AddDbContextPool<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DbConnection"), builder =>
            {
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });
        
        services.AddScoped<IApplicationDbContext>(p=> p.GetService<ApplicationDbContext>() ?? 
                                                      throw new InvalidOperationException("Context is not found"));
        
        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(ApplicationDependencyContainer).Assembly));
        });
        
        services.AddMemoryCache();
        services.AddOptions();
        services.AddHttpContextAccessor();
        services.AddResponseCaching();
    }
}