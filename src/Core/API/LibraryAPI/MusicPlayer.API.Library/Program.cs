using Serilog;
using ServerApp.Base;
using ServerApp.WebApp.Base.Configuration;
using ServerApp.WebApp.Base.Configuration.Setup;

try
{
    var builder = ServerAppBase.CreateApplication(args);
    builder.Host.UseSerilog(BaseConfiguration.ConfigureSerilog);
    
    builder.Services.ConfigureServicesFromAssembly(additionalServices: builder.Configuration);
    var app = builder.Build();
    ServerMessages.StartApplicationMessage();
    app.ConfigureFromAssembly();
    ServerMessages.Success();
    ServerMessages.ApplicationHostUrls();
    app.Run();
    return 0;
}
catch (Exception e)
{
    ServerMessages.ErrorMessage(e);
    return 1;
}
finally
{
    ServerMessages.StopApplication();
}

