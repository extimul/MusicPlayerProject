using FluentValidation.AspNetCore;
using Serilog;

namespace MusicPlayer.IdentityService;

public class Startup
{
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
 
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews()
            .AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining(typeof(Program));
                options.DisableDataAnnotationsValidation = true;
            });
    }
 
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseSerilogRequestLogging();
        
        app.UseIdentityServer();
        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseRouting();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
        });
    }
}