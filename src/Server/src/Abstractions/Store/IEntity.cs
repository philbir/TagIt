using AnyDiff;

namespace TagIt.Store;

public interface IEntity
{
    public Guid Id { get; set; }
}

public interface IEntityWithVersion : IEntity
{
    EntityVersion Version { get; set; }
}

public class EntityWithVersion
{
    [ID]
    public Guid Id { get; set; }

    public EntityVersion Version { get; set; }
}

public class EntityVersion
{
    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public static EntityVersion CreateNew(Guid userId)
    {
        return new EntityVersion
        {
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId,
            Version = 1,
        };
    }

    public static EntityVersion NewVersion(EntityVersion current, Guid userId)
    {
        var currentVersion = current is { } ? current.Version : 0;

        return new EntityVersion
        {
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId,
            Version = currentVersion + 1
        };
    }
}

public interface IEntityManager<T> where T : class, IEntityWithVersion, new()
{
    T CreateNew();
    Task<T> GetExistingOrCreateNewAsync(Guid? id, CancellationToken cancellationToken);
    Task<SaveEntityResult<T>> SaveAsync(T resource, CancellationToken cancellationToken);
    Task<SaveEntityResult<T>> SaveAsync(Guid? id, Action<T> setter, CancellationToken cancellationToken);
    void SetNewVersion(T resource);
    User User { get; }
}

public record SaveEntityResult<T>(T Entity, SaveEntityAction Action)
     where T : IEntityWithVersion, new()
{
    public ICollection<Difference>? Diff { get; init; }
}

public enum SaveEntityAction
{
    Inserted,
    Updated,
    UnChanged
}

public interface IStore<TEntity> where TEntity : class, IEntity, new()
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TEntity>> GetManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
}
