namespace MusicPlayer.API.Base.Entities;

public interface IBaseEntity<TKey> : IPersistenceObject<TKey>
{
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}