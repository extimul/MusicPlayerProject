using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicPlayer.API.Library.Domain.Entities.LibraryUnits;
using ServerApp.WebApp.Base.Persistence.EntityConfigurations;

namespace MusicPlayer.API.Library.Persistence.EntityConfigurations.LibraryUnits;

public class SoundtrackEntityConfiguration : LibraryApiEntityConfiguration<Soundtrack>
{
    public override void Configure(EntityTypeBuilder<Soundtrack> builder)
    {
        base.Configure(builder);

        builder
            .Property(x => x.MusicAlbumId)
            .HasColumnName("music_album_id");
        
        builder
            .HasOne(x => x.MusicAlbum)
            .WithMany(x => x.Soundtracks)
            .HasForeignKey(x => x.MusicAlbumId)
            .IsRequired();

        builder
            .HasOne(x => x.SoundtrackDetail)
            .WithOne(x => x.Soundtrack)
            .HasForeignKey<SoundtrackDetail>(x => x.SoundtrackId)
            .IsRequired();
    }
}