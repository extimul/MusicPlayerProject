namespace MusicPlayer.API.Base.Entities;

public interface IPersistenceObject<TKey>
{
    public TKey Id { get; set; }
}