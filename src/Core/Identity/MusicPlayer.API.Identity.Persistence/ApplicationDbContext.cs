using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MusicPlayer.API.Identity.Application.Interfaces;
using MusicPlayer.API.Identity.Domain.Entities;

namespace MusicPlayer.API.Identity.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<IdentityRole<long>>(e => e.ToTable("Roles"));
        builder.Entity<IdentityUserRole<long>>(e => e.ToTable("UserRoles"));
        builder.Entity<IdentityUserClaim<long>>(e => e.ToTable("UserClaims"));
        builder.Entity<IdentityUserLogin<long>>(e => e.ToTable("UserLogins"));
        builder.Entity<IdentityUserToken<long>>(e => e.ToTable("UserTokens"));
        builder.Entity<IdentityRoleClaim<long>>(e => e.ToTable("RoleClaims"));

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}