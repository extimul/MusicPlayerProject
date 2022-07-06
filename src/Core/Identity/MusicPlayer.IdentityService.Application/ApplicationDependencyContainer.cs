using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.IdentityService.Application.Behaviors;
using MusicPlayer.IdentityService.Application.Services.Login;
using MusicPlayer.IdentityService.Domain.Entities;

namespace MusicPlayer.IdentityService.Application;

public static class ApplicationDependencyContainer
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ILoginService<ApplicationUser>, LoginService>();

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssemblies(new[] {Assembly.GetExecutingAssembly()});
        services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidatorBehavior<,>));
    }
}