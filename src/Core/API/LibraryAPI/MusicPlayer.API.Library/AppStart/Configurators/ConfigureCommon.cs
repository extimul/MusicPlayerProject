using AutoMapper;
using EasyServiceConfigurator.AspNet;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MusicPlayer.API.Library.Extensions;
using Serilog;
using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Extensions;

namespace MusicPlayer.API.Library.AppStart.Configurators;

[ConfigurationLoadPriority(1)]
public class ConfigureCommon : IAppConfigurator
{
    public void Configure(IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
        var mapper = app.ApplicationServices.GetRequiredService<IMapper>();

        if (env.IsDevelopment())
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            app.UseDeveloperExceptionPage();
            app.UseSwagger(app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>());
        }
        else
        {
            mapper.ConfigurationProvider.CompileMappings();
        }

        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();
        app.UseAppForwardedHeaders();
        app.UseResponseCaching();
    }
}