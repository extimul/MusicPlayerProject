using ServerApp.WebApp.Base.Entities;

namespace MusicPlayer.API.Library.Domain.Entities.LibraryUnits;

/// <summary>
/// Сущность доп. информации о саундтреке
/// </summary>
public sealed class SoundtrackDetail : BaseEntity<long>
{
    /// <summary>
    /// Идентификатор саундтрека
    /// </summary>
    public long SoundtrackId { get; set; }
    
    /// <summary>
    /// Ссылка на саундтрек
    /// </summary>
    public Soundtrack Soundtrack { get; set; }
    
    /// <summary>
    /// Длина трека
    /// </summary>
    public long TrackLength { get; set; }
    
    /// <summary>
    /// Ссылка на трек в хранилище
    /// </summary>
    public string SourceUrl { get; set; }
}