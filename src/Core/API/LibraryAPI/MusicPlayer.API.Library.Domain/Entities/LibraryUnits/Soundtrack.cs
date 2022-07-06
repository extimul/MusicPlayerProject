using ServerApp.WebApp.Base.Entities;

namespace MusicPlayer.API.Library.Domain.Entities.LibraryUnits;

/// <summary>
/// Саундтрек, музыка, трек
/// </summary>
public sealed class Soundtrack : BaseEntity<long>, IAudioObject
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Идентификатор альбома к которому относится текущая песня
    /// </summary>
    public long MusicAlbumId { get; set; }
    
    /// <summary>
    /// Ссылка на объект альбома
    /// </summary>
    public MusicAlbum MusicAlbum { get; set; }
    
    /// <summary>
    /// Ссылка на доп. информацию о саундтреке
    /// </summary>
    public SoundtrackDetail SoundtrackDetail { get; set; }
}