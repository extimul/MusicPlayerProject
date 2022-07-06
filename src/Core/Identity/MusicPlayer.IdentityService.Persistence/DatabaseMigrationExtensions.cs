using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MusicPlayer.IdentityService.Persistence;

public static class DatabaseMigrationExtensions
{
    public static void Migrate(this IApplicationBuilder app)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        
        Log.Information("Start migration");

        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();

        using (var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>())
        {
            persistedGrantDbContext.Database.Migrate();
        }

        using (var configDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
        {
            configDbContext.Database.Migrate();

            // var client = configDbContext.Clients.First();
            // configDbContext.Clients.Remove(client);
            // configDbContext.SaveChanges();
            
            var clients = configuration.GetSection("Clients").Get<List<Client>>();

            if (!configDbContext.Clients.Any())
            {
                Log.Information("Clients is empty");
                Log.Information("Start adding Clients");
                clients.ForEach(x => configDbContext.Clients.Add(x.ToEntity()));
                Log.Information("Successfully added {Count} clients", clients.Count);
            }
            else if(configDbContext.Clients.Count() < clients.Count)
            {
                Log.Information("New client {ClientName} found", clients.Last().ClientName);
                configDbContext.Clients.Add(clients.Last().ToEntity());
            }
            else
            {
                Log.Information("Not found new Clients");
            }

            configDbContext.SaveChanges();
            
            if (!configDbContext.IdentityResources.Any())
            {
                Log.Information("IdentityResources is empty");
                Log.Information("Start adding IdentityResources");
                IdentityConfig.IdentityResources.ToList().ForEach(x => configDbContext.IdentityResources.Add(x.ToEntity()));
                Log.Information("Successfully added {Count} IdentityResources", IdentityConfig.IdentityResources.Count());
            }
            else if(configDbContext.IdentityResources.Count() < IdentityConfig.IdentityResources.Count())
            {
                Log.Information("New IdentityResource {Name} found",  IdentityConfig.IdentityResources.Last().Name);
                configDbContext.IdentityResources.Add(IdentityConfig.IdentityResources.Last().ToEntity());
            }
            else
            {
                Log.Information("Not found new IdentityResources");
            }
            
            configDbContext.SaveChanges();

            if (!configDbContext.ApiResources.Any())
            {
                Log.Information("ApiResources is empty");
                Log.Information("Start adding ApiResources");
                IdentityConfig.ApiResources.ToList().ForEach(x => configDbContext.ApiResources.Add(x.ToEntity()));
                Log.Information("Successfully added {Count} ApiResources", IdentityConfig.ApiResources.Count());
            }
            else if(configDbContext.ApiResources.Count() < IdentityConfig.ApiResources.Count())
            {
                Log.Information("New ApiResources ({Name} found",  IdentityConfig.ApiResources.Last().Name);
                configDbContext.ApiResources.Add(IdentityConfig.ApiResources.Last().ToEntity());
            }
            else
            {
                Log.Information("Not found new ApiResources");
            }

            configDbContext.SaveChanges();
            
            if (!configDbContext.ApiScopes.Any())
            {
                Log.Information("ApiScopes is empty");
                Log.Information("Start adding ApiScopes");
                IdentityConfig.ApiScopes.ToList().ForEach(x => configDbContext.ApiScopes.Add(x.ToEntity()));
                Log.Information("Successfully added {Count} ApiResources", IdentityConfig.ApiScopes.Count());
            }
            else if(configDbContext.ApiScopes.Count() < IdentityConfig.ApiScopes.Count())
            {
                Log.Information("New ApiScopes ({Name} found",  IdentityConfig.ApiScopes.Last().Name);
                configDbContext.ApiScopes.Add(IdentityConfig.ApiScopes.Last().ToEntity());
            }
            else
            {
                Log.Information("Not found new ApiScopes");
            }
            
            configDbContext.SaveChanges();
        }
        
        Log.Information("Migration finished");
    }

    // private static void AddConfig<TEntity, TModel>(DbSet<TEntity> set, List<TModel> localCollection)
    //     where TEntity : class 
    //     where TModel : I
    // {
    //     if (_context is null) return;
    //     
    //     if (_context.Set<TEntity>().Any())
    //     {
    //         localCollection.ForEach(x => _context.Set<TEntity>().Add(x));
    //     }
    //     else if(_context.Set<TEntity>().Count() < localCollection.Count)
    //     {
    //         _context.Set<TEntity>().Add(localCollection.Last().ToEntity());
    //     }
    //
    //     _context.SaveChangesAsync();
    // }
}