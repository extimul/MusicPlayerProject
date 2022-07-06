using Microsoft.EntityFrameworkCore;
using MusicPlayer.API.Library.Domain.Entities.LibraryUnits;

namespace MusicPlayer.API.Library.Persistence.Contexts;

public interface ILibraryDbContext
{
    public DbSet<MusicAlbum> MusicAlbums { get; set; }
    
    public DbSet<Playlist> Playlists { get; set; }
    
    public DbSet<Soundtrack> Soundtracks { get; set; }
}