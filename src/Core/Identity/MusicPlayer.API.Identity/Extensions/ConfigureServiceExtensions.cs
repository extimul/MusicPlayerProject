namespace MusicPlayer.API.Identity.Extensions;

public static class ConfigureServiceExtensions
{
    public static IApplicationBuilder EnableSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });

        return app;
    }
}