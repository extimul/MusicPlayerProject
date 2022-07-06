using AutoMapper;
using MusicPlayer.IdentityService.Persistence;
using Serilog;
using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Configuration.Setup;

namespace MusicPlayer.IdentityService.AppStart.Configurators;

[ConfigurationLoadPriority(2)]
public class ConfigureCommon : IAppConfigurator
{
    public void Configure(IApplicationBuilder app)
    {
        app.Migrate();
        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
        var mapper = app.ApplicationServices.GetRequiredService<IMapper>();

        if (env.IsDevelopment())
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            mapper.ConfigurationProvider.CompileMappings();
        }

        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.UseSerilogRequestLogging();
        app.UseResponseCaching();
    }
}