using Microsoft.AspNetCore.Builder;

namespace ServerApp.WebApp.Base.Configuration.Setup;

public interface IAppConfigurator
{
    public void Configure(IApplicationBuilder app);
}