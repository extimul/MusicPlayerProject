using IdentityServer4.AspNetIdentity;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.IdentityService.Domain.Entities;
using MusicPlayer.IdentityService.Persistence;
using ServerApp.Base.Extensions;
using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Configuration.Setup;

namespace MusicPlayer.IdentityService.AppStart.ConfigureServices;

[ConfigurationLoadPriority(2)]
public class ConfigureServicesAuthentication : IAppSevicesConfigurator
{
    public void ConfigureServices(IServiceCollection services, params object[]? additionalServices)
    {
        var migrationsAssembly = typeof(ApplicationDbContext).Assembly.FullName;
        var configuration = additionalServices.GetConfigureParam<ConfigurationManager>();
        var connectionString = configuration.GetConnectionString("DbConnection");
        
        services.AddIdentityServer(s =>
            {
                s.UserInteraction.LoginUrl = "/account/signin";
                
                s.IssuerUri = "null";
                s.Events.RaiseErrorEvents = true;
                s.Events.RaiseInformationEvents = true;
                s.Events.RaiseFailureEvents = true;
                s.Events.RaiseSuccessEvents = true;
            })
            .AddConfigurationStore(opt =>
            {
                opt.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(migrationsAssembly);
                        optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
            })
            .AddOperationalStore(opt => 
                opt.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(migrationsAssembly);
                        optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    }))
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<ApplicationUser>()
            .Services.AddTransient<IProfileService, ProfileService<ApplicationUser>>();
    }
}