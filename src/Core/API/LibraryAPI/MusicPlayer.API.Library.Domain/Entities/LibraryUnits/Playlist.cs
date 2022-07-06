using MusicPlayer.API.Library.Domain.Entities.Users;
using ServerApp.WebApp.Base.Entities;

namespace MusicPlayer.API.Library.Domain.Entities.LibraryUnits;

/// <summary>
/// Сущность плейлиста
/// </summary>
public sealed class Playlist : BaseEntity<long>, IAudioObject
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Саундтреки 
    /// </summary>
    public ICollection<Soundtrack> Soundtracks { get; set; }
    
    /// <summary>
    /// Авторы плейлиста
    /// </summary>
    public ICollection<Person> Authors { get; set; }
}