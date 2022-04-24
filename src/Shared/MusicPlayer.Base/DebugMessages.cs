using Serilog;

namespace MusicPlayer.Base;

public static class DebugMessages
{
    public static void StartMessage(string appName)
    {
        Log.Information("Starting {AppName}", appName);
        Log.Information("Configuring...");
        Log.Information("Current platform: {OS}", App.GetCurrentPlatform());
    }

    public static void SuccessfullyStart(string appName)
    {
        Log.Information("{AppName} successfully started!", appName);
    }

    public static void ErrorMessage(Exception e, string appName)
    {
        Log.Error(e, "The {AppName} failed to start correctly", appName);
    }

    public static void StopMessage(string appName)
    {
        Log.Information("Stopping {AppName}", appName);
        Log.CloseAndFlush();
    }
}