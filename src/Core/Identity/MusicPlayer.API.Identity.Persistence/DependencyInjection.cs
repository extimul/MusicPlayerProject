using System.Reflection;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.API.Base.Exceptions;
using MusicPlayer.API.Identity.Application.Interfaces;
using MusicPlayer.API.Identity.Domain.Entities;

namespace MusicPlayer.API.Identity.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, builder =>
            {
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });

        services.AddScoped<IApplicationDbContext>(p=> p.GetService<ApplicationDbContext>() ?? 
                                                              throw new BaseAppException("Context is not found"));
        
        return services;
    }
    
    public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationsAssembly = Assembly.GetExecutingAssembly().FullName;
        var connectionString = configuration["DbConnection"];

        var passwordOptions = configuration.GetSection($"IdentityServerOptions:{nameof(PasswordOptions)}").Get<PasswordOptions>();
        var userOptions = configuration.GetSection($"IdentityServerOptions:{nameof(UserOptions)}").Get<UserOptions>();
        var signInOptions = configuration.GetSection($"IdentityServerOptions:{nameof(SignInOptions)}").Get<SignInOptions>();
        var lockoutOptions = configuration.GetSection($"IdentityServerOptions:{nameof(LockoutOptions)}").Get<LockoutOptions>();
        
        services.AddIdentity<ApplicationUser, IdentityRole<long>>(config =>
            {
                config.Password.RequiredLength = passwordOptions.RequiredLength;
                config.Password.RequireDigit = passwordOptions.RequireDigit;
                config.Password.RequireNonAlphanumeric = passwordOptions.RequireNonAlphanumeric;
                config.Password.RequireUppercase = passwordOptions.RequireUppercase;
                config.Password.RequireLowercase = passwordOptions.RequireLowercase;
                config.Password.RequiredUniqueChars = passwordOptions.RequiredUniqueChars;

                config.User.RequireUniqueEmail = userOptions.RequireUniqueEmail;
                config.User.AllowedUserNameCharacters = userOptions.AllowedUserNameCharacters;

                config.SignIn.RequireConfirmedEmail = signInOptions.RequireConfirmedEmail;
                config.SignIn.RequireConfirmedPhoneNumber = signInOptions.RequireConfirmedPhoneNumber;
                config.SignIn.RequireConfirmedAccount = signInOptions.RequireConfirmedAccount;

                config.Lockout.AllowedForNewUsers = lockoutOptions.AllowedForNewUsers;
                config.Lockout.DefaultLockoutTimeSpan = lockoutOptions.DefaultLockoutTimeSpan;
                config.Lockout.MaxFailedAccessAttempts = lockoutOptions.MaxFailedAccessAttempts;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddIdentityServer(s =>
            {
                s.IssuerUri = "null";
                s.Events.RaiseErrorEvents = true;
                s.Events.RaiseInformationEvents = true;
                s.Events.RaiseFailureEvents = true;
                s.Events.RaiseSuccessEvents = true;
            })
            .AddConfigurationStore(opt =>
            {
                opt.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
                    opt =>
                    {
                        opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
            })
            .AddOperationalStore(opt => 
                opt.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
                    opt =>
                    {
                        opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    }))
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<ApplicationUser>()
            .Services.AddTransient<IProfileService, ProfileService<ApplicationUser>>();
        
        return services;
    }
}