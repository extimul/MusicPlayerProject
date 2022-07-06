using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.API.Library.Domain.Entities.LibraryUnits;
using MusicPlayer.API.Library.Domain.Entities.Users;
using ServerApp.WebApp.Base.Persistence;

namespace MusicPlayer.API.Library.Persistence.Contexts;

public class LibraryApiDbContext : DbContext, IUserDbContext, ILibraryDbContext, IBaseDbContext
{
    public DbSet<Person> Persons { get; set; }
    
    public DbSet<Artist> Artists { get; set; }
    
    public DbSet<MusicAlbum> MusicAlbums { get; set; }
    
    public DbSet<Playlist> Playlists { get; set; }
    
    public DbSet<Soundtrack> Soundtracks { get; set; }

    public LibraryApiDbContext(DbContextOptions<LibraryApiDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}