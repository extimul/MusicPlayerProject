using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MusicPlayer.API.Library.Extensions;

public static class ServiceConfigureExtensions
{
    public static void UseSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(settings =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                settings.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
                settings.HeadContent = $"{ThisAssembly.Git.Branch.ToUpper()} {ThisAssembly.Git.Commit.ToUpper()}";
                settings.DocumentTitle = ServerApp.Base.ServerAppBase.Title;
                settings.DefaultModelExpandDepth(0);
                settings.DefaultModelRendering(ModelRendering.Model);
                settings.DefaultModelsExpandDepth(0);
                settings.DocExpansion(DocExpansion.None);
                settings.OAuthClientId("api_swagger");
                settings.OAuthClientSecret("api_swagger");
                settings.OAuthUsePkce();
                settings.OAuthScopeSeparator(" ");
                settings.DisplayRequestDuration();
            }
        });
    }
}