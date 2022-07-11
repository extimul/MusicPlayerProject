using EasyServiceConfigurator.AspNet;
using ServerApp.WebApp.Base.Common.Attributes;

namespace MusicPlayer.API.Library.AppStart.Configurators;

[ConfigurationLoadPriority(3)]
public class ConfigureEndpoints : IAppConfigurator
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}