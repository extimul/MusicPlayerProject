using MusicPlayer.API.Library.Domain.Entities.LibraryUnits;
using ServerApp.WebApp.Base.Entities;

namespace MusicPlayer.API.Library.Domain.Entities.Users;

/// <summary>
/// Сущность исполнителя
/// </summary>
public sealed class Artist : BaseEntity<long>
{
    public long PersonId { get; set; }
    
    public Person Person { get; set; }
    
    public ICollection<MusicAlbum> Albums { get; set; }
}