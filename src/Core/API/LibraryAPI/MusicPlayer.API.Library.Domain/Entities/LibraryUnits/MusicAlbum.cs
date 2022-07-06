using MusicPlayer.API.Library.Domain.Entities.Users;
using MusicPlayer.API.Library.Domain.Types;
using ServerApp.WebApp.Base.Entities;

namespace MusicPlayer.API.Library.Domain.Entities.LibraryUnits;

/// <summary>
/// Музыкальный альбом
/// </summary>
public sealed class MusicAlbum : BaseEntity<long>, IAudioObject
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Тип альбома
    /// </summary>
    public AlbumType AlbumType { get; set; }
    
    /// <summary>
    /// Список авторов
    /// </summary>
    public ICollection<Artist> Artists { get; set; }
    
    /// <summary>
    /// Саундтреки
    /// </summary>
    public ICollection<Soundtrack> Soundtracks { get; set; }
}