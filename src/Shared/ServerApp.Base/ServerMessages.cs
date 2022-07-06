using Serilog;

namespace ServerApp.Base;

public static class ServerMessages
{
    public static void StartApplicationMessage()
    {
        Log.Information("Starting {AppName}", ServerAppBase.Title);
        Log.Information("Configuring...");
        Log.Information("Current platform: {OS}", ServerAppBase.GetCurrentPlatform());
        Log.Information("Current environment: {Environment}", ServerAppBase.CurrentEnvironment);
    }

    public static void Success()
    {
        Log.Information("{AppName} successfully started!", ServerAppBase.Title);
    }

    public static void ErrorMessage(Exception e)
    {
        Log.Error(e, "The {AppName} failed to start correctly", ServerAppBase.Title);
    }

    public static void StopApplication()
    {
        Log.Information("Stopping {AppName}", ServerAppBase.Title);
        Log.CloseAndFlush();
    }

    public static void ApplicationHostUrls()
    {
        var hosts = ServerAppBase.HostUrls;
        Log.Information("Application urls:");

        if (!hosts.Any())
        {
            Log.Information("Hosts are not set");
            return;
        }

        for (var i = 0; i < hosts.Length; i++)
        {
            Log.Information("{Number}.{Host}", i + 1,hosts[i]);
        }
    }
}