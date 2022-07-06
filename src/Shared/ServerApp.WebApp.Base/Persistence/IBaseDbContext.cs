namespace ServerApp.WebApp.Base.Persistence;

public interface IBaseDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}