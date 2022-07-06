namespace ServerApp.WebApp.Base.Entities;

public interface IBaseUser<TKey>
{
    public TKey ExternalId { get; set; }
}