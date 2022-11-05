using AnyClone;
using AnyDiff;
using AnyDiff.Extensions;

namespace TagIt.Store;

public class EntityManager<T> : IEntityManager<T> where T : class, IEntityWithVersion, new()
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IAuditStore _auditStore;

    public EntityManager(
        IUserContextAccessor userContextAccessor,
        IStore<T> store,
        IAuditStore auditStore)
    {
        Store = store;
        _userContextAccessor = userContextAccessor;
        _auditStore = auditStore;
    }

    public IStore<T>? Store { get; }

    public User User
    {
        get
        {
            return _userContextAccessor.Context?.User ??
                   throw CouldNotAccessUserContextException.New();
        }
    }

    public IEntity? Original { get; private set; }

    public T CreateNew()
    {
        var resource = new T();
        resource.Version = EntityVersion.CreateNew(User.Id);
        resource.Id = Guid.NewGuid();

        return resource;
    }

    public void SetNewVersion(T resource)
    {
        resource.Version = EntityVersion.NewVersion(resource.Version, User.Id);
    }

    public async Task<T> GetExistingOrCreateNewAsync(
        Guid? id,
        CancellationToken cancellationToken)
    {
        IEntity resource;

        if (id.HasValue)
        {
            resource = await Store!.GetByIdAsync(id.Value, cancellationToken);
            Original = resource.Clone();
        }
        else
        {
            resource = CreateNew();
        }

        return (T)resource;
    }

    public async Task<SaveEntityResult<T>> SaveAsync(
        Guid? id,
        Action<T> setter,
        CancellationToken cancellationToken)
    {
        T entity = await GetExistingOrCreateNewAsync(id, cancellationToken);
        setter(entity);

        return await SaveAsync(entity, cancellationToken);
    }

    public async Task<SaveEntityResult<T>> SaveAsync(
        T resource,
        CancellationToken cancellationToken)
    {
        ICollection<Difference>? diff = null;
        SaveEntityAction action = SaveEntityAction.Inserted;

        if (Original is { })
        {
            diff = Original.Diff(resource);

            if (diff.Any())
            {
                resource.Version = EntityVersion.NewVersion(
                    resource.Version,
                    User.Id);
                action = SaveEntityAction.Updated;
            }
            else
            {
                action = SaveEntityAction.UnChanged;
            }
        }

        if (action != SaveEntityAction.UnChanged)
        {
            await Store!.SaveAsync(resource, cancellationToken);

            var audit = new EntityAuditEvent
            {
                Id = Guid.NewGuid(),
                EntityId = resource.Id,
                Version = resource.Version.Version,
                EntityName = resource.GetType().Name,
                UserId = User.Id,
                Timestamp = DateTime.UtcNow,
                Action = action.ToString(),
                Changes = CreateChanges(diff)
            };

            await _auditStore.InsertAsync(audit, cancellationToken);
        }

        return new SaveEntityResult<T>(resource, action)
        {
            Diff = diff
        };
    }

    private IEnumerable<EntityChange> CreateChanges(ICollection<Difference>? diffs)
    {
        var changes = new List<EntityChange>();

        if (diffs is { })
        {
            foreach (Difference diff in diffs)
            {
                changes.Add(new EntityChange
                {
                    Property = diff.Property,
                    Path = diff.Path,
                    Before = diff.LeftValue?.ToString(),
                    After = diff.RightValue?.ToString(),
                    Delta = diff.Delta?.ToString(),
                    ArrayIndex = diff.ArrayIndex
                });
            }
        }

        return changes;
    }
}
