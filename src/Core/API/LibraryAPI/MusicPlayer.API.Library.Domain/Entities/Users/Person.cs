using MusicPlayer.API.Library.Domain.Entities.LibraryUnits;
using ServerApp.WebApp.Base.Entities;

namespace MusicPlayer.API.Library.Domain.Entities.Users;

/// <summary>
/// Сущность пользователя
/// </summary>
public sealed class Person : BaseEntity<long>, IBaseUser<long>
{
    /// <summary>
    /// Внешний Id
    /// </summary>
    public long ExternalId { get; set; }
    
    public ICollection<Playlist> Playlists { get; set; }
}