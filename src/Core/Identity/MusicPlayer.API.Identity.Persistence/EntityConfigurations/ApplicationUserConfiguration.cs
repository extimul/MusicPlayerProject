using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicPlayer.API.Base.Persistence;
using MusicPlayer.API.Identity.Domain.Entities;

namespace MusicPlayer.API.Identity.Persistence.EntityConfigurations;

public class ApplicationUserConfiguration : BaseEntityConfiguration<ApplicationUser, long>
{
    public override void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        
        base.Configure(builder);

        builder
            .Property(x => x.Name)
            .HasColumnName("name");

        builder
            .Property(x => x.Surname)
            .HasColumnName("surname");
    }
}