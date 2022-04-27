using MusicPlayer.Base;
using MusicPlayer.WebUI.Authorization.Core;
using Serilog;

var appName = App.GetName();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, cfg) =>
        cfg.ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddCoreServices();
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();

    var app = builder.Build();

    DebugMessages.StartMessage(appName);

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
}
catch (Exception e)
{
    DebugMessages.ErrorMessage(e, appName);
}
finally
{
    DebugMessages.StopMessage(appName);
}

