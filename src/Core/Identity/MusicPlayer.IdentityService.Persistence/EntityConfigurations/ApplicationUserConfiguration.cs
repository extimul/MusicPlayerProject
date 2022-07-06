using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicPlayer.IdentityService.Domain.Entities;
using ServerApp.WebApp.Base.Persistence.EntityConfigurations;

namespace MusicPlayer.IdentityService.Persistence.EntityConfigurations;

public class ApplicationUserConfiguration : BaseEntityConfiguration<ApplicationUser, long>
{
    public override void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        
        base.Configure(builder);

        builder
            .Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();
    }
}