using Microsoft.Extensions.DependencyInjection;

namespace ServerApp.WebApp.Base.Configuration.Setup;

public interface IAppSevicesConfigurator
{
    public void ConfigureServices(IServiceCollection services, params object[]? additionalServices);
}