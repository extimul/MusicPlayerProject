using System.Reflection;
using System.Runtime.InteropServices;

namespace MusicPlayer.Base;

public static class App
{
    public static string GetName()
        => Assembly.GetCallingAssembly().GetName().Name ?? "UNKNOWN_APP";

    public static string GetCurrentPlatform()
        => RuntimeInformation.OSDescription;
}