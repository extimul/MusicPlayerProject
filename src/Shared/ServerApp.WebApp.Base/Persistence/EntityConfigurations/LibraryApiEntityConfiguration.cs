using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.WebApp.Base.Entities;

namespace ServerApp.WebApp.Base.Persistence.EntityConfigurations;

/// <summary>
/// Configuration of types implementing <see cref="IAudioObject"/>
/// </summary>
/// <typeparam name="TEntity">The object that implements <see cref="IAudioObject"/></typeparam>
/// <typeparam name="TKey">Id type(default <see cref="long"/>)</typeparam>
public class LibraryApiEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity, long>
    where TEntity : class, IAudioObject, IBaseEntity<long>
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(35)
            .IsRequired();
    }
}