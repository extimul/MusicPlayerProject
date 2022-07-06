using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace ServerApp.Base;

public class ServerAppBase
{
    private static IDictionary _environmentVariables;

    public static string? Title { get; private set; } = "Unknown_APP";

    public static string CurrentEnvironment { get; private set; } = null!;
    
    public static string[]? HostUrls { get; private set; }
    
    public static string? SecuredUrl { get; private set; }

    public static ApiVersion ApiVersion { get; set; } = new(1, 0);

    public static WebApplicationBuilder CreateApplication(string[] args)
    {
        _environmentVariables = Environment.GetEnvironmentVariables();
        Title = Assembly.GetCallingAssembly().GetName().Name;
        CurrentEnvironment = GetEnvironment();
        HostUrls = GetHostUrls();
        SecuredUrl = GetSecuredUrl();
        
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
        {
            ApplicationName = Title,
            ContentRootPath = Directory.GetCurrentDirectory(),
            EnvironmentName = CurrentEnvironment,
            Args = args
        });

        return builder;
    }

    private static string[]? GetHostUrls()
        => _environmentVariables?["ASPNETCORE_URLS"]?
            .ToString()
            ?.Split(';');

    private static string? GetSecuredUrl()
        => HostUrls.FirstOrDefault(x => x.Contains("https"));

    private static string GetEnvironment()
        => _environmentVariables?["ASPNETCORE_ENVIRONMENT"]?.ToString() ?? "Development";
    
    public static string GetCurrentPlatform()
        => RuntimeInformation.OSDescription;
}