using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace MusicPlayer.WebUI.Authorization.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(new[] {Assembly.GetExecutingAssembly()});
        
        return services;
    }
}