using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicPlayer.API.Library.Domain.Entities.LibraryUnits;
using ServerApp.WebApp.Base.Persistence.EntityConfigurations;

namespace MusicPlayer.API.Library.Persistence.EntityConfigurations.LibraryUnits;

public class SoundtrackDetailEntityConfiguration : BaseEntityConfiguration<SoundtrackDetail, long>
{
    public override void Configure(EntityTypeBuilder<SoundtrackDetail> builder)
    {
        base.Configure(builder);

        builder
            .Property(x => x.SoundtrackId)
            .HasColumnName("soundtrack_id");

        builder
            .Property(x => x.TrackLength)
            .HasColumnName("track_length");

        builder
            .Property(x => x.SourceUrl)
            .HasColumnName("source_url")
            .HasMaxLength(1000)
            .IsRequired();
    }
}