using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicPlayer.API.Identity.Persistence.DbContextFactories;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=db_mp_identity;Username=postgres;Password=admin1234", builder =>
        {
            builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        });

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}