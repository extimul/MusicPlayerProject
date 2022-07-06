using Microsoft.EntityFrameworkCore;
using MusicPlayer.API.Library.Domain.Entities.Users;

namespace MusicPlayer.API.Library.Persistence.Contexts;

public interface IUserDbContext
{
    public DbSet<Person> Persons { get; set; }
    
    public DbSet<Artist> Artists { get; set; }
}