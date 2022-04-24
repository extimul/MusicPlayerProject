using MusicPlayer.API.Identity.Extensions;
using Serilog;

namespace MusicPlayer.API.Identity;

public class Startup
{
    public IConfiguration Configuration { get; }

    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
 
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddServices(Configuration);
        services.AddControllers()
            .AddNewtonsoftJson();
        services.AddSwagger();
    }
 
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.EnableSwagger(); 
            app.UseDeveloperExceptionPage();
        }
        
        app.UseIdentityServer();
        app.UseSerilogRequestLogging();
        
        app.UseHttpsRedirection();
        app.UseRouting();
 
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}