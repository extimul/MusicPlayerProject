using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ServerApp.Base.Extensions;
using ServerApp.WebApp.Base.Common.Attributes;
using ServerApp.WebApp.Base.Configuration.Setup;

namespace MusicPlayer.API.Library.AppStart.ServiceConfigurators;

[ConfigurationLoadPriority(2)]
public class ConfigureServicesAuthentication : IAppSevicesConfigurator
{
    public void ConfigureServices(IServiceCollection services, params object[]? additionalServices)
    {
        if (additionalServices is null && additionalServices.Any())
        {
            var configuration = additionalServices.GetConfigureParam<IConfiguration>();
            var url = configuration.GetSection("IdentityServer").GetValue<string>("Url");
            
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.Authority = url;
                    options.EnableCaching = true;
                    options.RequireHttpsMetadata = true;
                });

            services.AddAuthorization();
        }
    }
}