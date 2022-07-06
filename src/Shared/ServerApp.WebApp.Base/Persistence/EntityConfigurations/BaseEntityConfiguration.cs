using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.WebApp.Base.Entities;

namespace ServerApp.WebApp.Base.Persistence.EntityConfigurations;

public class BaseEntityConfiguration<TEntity,TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IBaseEntity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Id)
            .IsUnique();
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("now()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("now()");
    }
}