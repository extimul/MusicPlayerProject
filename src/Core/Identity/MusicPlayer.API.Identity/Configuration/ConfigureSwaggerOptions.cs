using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MusicPlayer.Base;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MusicPlayer.API.Identity.Configuration;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Version = "v1",
                Title = $"{App.GetName()}",
                Description  = "Identity Server",
                Contact = new OpenApiContact
                {
                    Name = "ExTimul",
                    Url = new Uri("https://github.com/extimul/MusicPlayerProject/tree/dev-identity-server")
                }
            });
    }
}