namespace MusicPlayer.API.Base.Entities;

public class BaseEntity<TKey> : IBaseEntity<TKey>
{
    public TKey Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}