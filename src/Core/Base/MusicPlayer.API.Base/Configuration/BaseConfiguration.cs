using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;

namespace MusicPlayer.API.Base.Configuration;

public static class BaseConfiguration
{
    public static IConfigurationBuilder CreateConfig(string directory, string environment)
    {
        var appSettingFileName = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ?
            "appsettings.Linux.json" : "appsettings.json";
        
        var builder = new ConfigurationBuilder()
            .SetBasePath(!string.IsNullOrEmpty(directory) ?
                directory : 
                throw new ArgumentException("Directory can't be null or empty string"))
            .AddJsonFile(appSettingFileName, optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables();

        return builder;
    }

    public static ConfigurationManager AddConfigFiles(this ConfigurationManager configuration, string directory, string environment)
    {
        var appSettingFileName = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ?
            "appsettings.Linux.json" : "appsettings.json";
        
        configuration
            .SetBasePath(!string.IsNullOrEmpty(directory) ?
                directory : 
                throw new ArgumentException("Directory can't be null or empty string"))
            .AddJsonFile(appSettingFileName, optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables();
        
        return configuration;
    }
}