using System.Reflection;
using MusicPlayer.API.Base.Utils;
using MusicPlayer.API.Identity.Extensions;
using MusicPlayer.API.Identity.Persistence;
using Serilog;

var currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    ApplicationName = Assembly.GetExecutingAssembly().GetName().Name,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = currentEnvironment,
    Args = args
});

builder.Configuration.AddClients();

builder.Host.UseSerilog((ctx, cfg) =>
    cfg.ReadFrom.Configuration(ctx.Configuration));

var appName = App.GetName();

try
{
    Log.Information("Starting {AppName}", appName);
    Log.Information("Configuring...");
    Log.Information("Current platform: {OS}", App.GetCurrentPlatform());

    builder.Services.AddServices(builder.Configuration);
    builder.Services.AddControllers()
        .AddNewtonsoftJson();
    builder.Services.AddSwagger();
    
    var app = builder.Build();

    DatabaseMigrator.Migrate(builder.Configuration, app);
    
    if (builder.Environment.IsDevelopment())
    {
        app.EnableSwagger();
        app.UseDeveloperExceptionPage();
    }
    
    app.UseIdentityServer();
    
    app.UseSerilogRequestLogging();
    app.UseRouting();
    app.MapControllers();
    
    Log.Information("{AppName} successfully started!", appName);
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "The {AppName} failed to start correctly", appName);
}
finally
{
    Log.Information("Stopping {AppName}", appName);
    Log.CloseAndFlush();
}


// using System.Reflection;
// using MusicPlayer.API.Base.Configuration;
// using MusicPlayer.API.Identity.Extensions;
// using Serilog;
//
// namespace MusicPlayer.API.Identity;
//
// public class Program
// {
//     public static void Main(string[] args)
//     {
//         var currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
//         var configuration = BaseConfiguration.CreateConfig(Environment.CurrentDirectory, currentEnvironment)
//             .AddClients()
//             .Build();
//
//         Log.Logger = new LoggerConfiguration()
//             .ReadFrom.Configuration(configuration)
//             .CreateLogger();
//
//         try
//         {
//             var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
//                 
//             Log.Information("Starting {AssemblyName}", assemblyName);
//             CreateHostBuilder(args).Build().Run();
//         }
//         catch (Exception e)
//         {
//             Log.Fatal(e, "The application failed to start correctly");
//         }
//         finally
//         {
//             Log.CloseAndFlush();
//         }
//     }
//
//     public static IHostBuilder CreateHostBuilder(string[] args) =>
//         Host.CreateDefaultBuilder(args)
//             .UseSerilog()
//             .ConfigureWebHostDefaults(webBuilder =>
//             {
//                 webBuilder.UseStartup<Startup>();
//             });
// }
