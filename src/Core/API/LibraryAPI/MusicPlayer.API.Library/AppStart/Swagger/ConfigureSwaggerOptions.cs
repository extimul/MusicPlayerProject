using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ServerApp.WebApp.Base.Common.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MusicPlayer.API.Library.AppStart.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly IConfiguration _configuration;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
    {
        _provider = provider;
        _configuration = configuration;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            var apiVersion = description.ApiVersion.ToString();
            options.SwaggerDoc(description.GroupName,
                new OpenApiInfo
                {
                    Version = apiVersion,
                    Title = ServerApp.Base.ServerAppBase.Title,
                    Description = "API веб-плеера",
                    TermsOfService = new Uri("https://devalphasoftware.atlassian.net/jira/software/projects/MP/boards/2"),
                });

            options.TagActionsBy(api =>
            {
                string tag;
                if (api.ActionDescriptor is ControllerActionDescriptor descriptor)
                {
                    var attribute = descriptor.EndpointMetadata.OfType<FeatureGroupNameAttribute>().FirstOrDefault();
                    tag = attribute?.GroupName ?? descriptor.ControllerName;
                }
                else
                {
                    tag = api.RelativePath!;
                }

                var tags = new List<string>();
                if (!string.IsNullOrEmpty(tag))
                {
                    tags.Add(tag);
                }
                return tags;
            });

            options.ResolveConflictingActions(x => x.First());

            var url = _configuration.GetSection("IdentityServer:Url").Get<string>();

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.OAuth2,
                In = ParameterLocation.Header,
                OpenIdConnectUrl =  new Uri($"{url}/.well-known/openid-configuration", UriKind.Absolute),
                BearerFormat = "JWT",
                Scheme = "Bearer",
                Flows = new OpenApiOAuthFlows()
                {
                    AuthorizationCode = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{url}/connect/authorize", UriKind.Absolute),
                        TokenUrl = new Uri($"{url}/connect/token", UriKind.Absolute),
                        Scopes = new Dictionary<string, string>()
                        {
                            { "monitoringservice.webapi.fullaccess", "Default scope" },
                            { "openid", "OpenId scope" },
                            { "profile", "Profile scope" },
                            { "email", "Email scope" }
                        },
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    Array.Empty<string>()
                }
            });
            
            options.CustomOperationIds(apiDescription => apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
                ? methodInfo.Name
                : null);
        }
    }
}