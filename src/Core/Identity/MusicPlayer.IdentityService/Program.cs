using MusicPlayer.IdentityService.Extensions;
using Serilog;
using ServerApp.Base;
using ServerApp.WebApp.Base.Configuration;
using ServerApp.WebApp.Base.Configuration.Setup;

try
{
    // Create web app instance
    var builder = ServerAppBase.CreateApplication(args);
    builder.Configuration.AddClients();
    builder.Host.UseSerilog(BaseConfiguration.ConfigureSerilog);
    
    // Collect and execute all services configurators
    builder.Services.ConfigureServicesFromAssembly(additionalServices: builder.Configuration);

    // Build app
    var app = builder.Build();
    
    //Message to console
    ServerMessages.StartApplicationMessage();

    //Collect and execute all configurators
    app.ConfigureFromAssembly();

    // Messages
    ServerMessages.Success();
    ServerMessages.ApplicationHostUrls();
    
    //Run app
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


// using System.Reflection;
// using MusicPlayer.API.Base.Configuration;
// using MusicPlayer.IdentityService.Extensions;
// using Serilog;
//
// namespace MusicPlayer.IdentityService;
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
