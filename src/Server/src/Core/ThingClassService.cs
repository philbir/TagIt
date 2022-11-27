using TagIt.Store;

namespace TagIt;

public class ThingClassService : IThingClassService
{
    private readonly IEntityManager<ThingType> _entityManager;
    private readonly IThingClassStore _store;

    public ThingClassService(
        IEntityManager<ThingType> entityManager,
        IThingClassStore store)
    {
        _entityManager = entityManager;
        _store = store;
    }

    public Task<ThingClass> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _store.GetByIdAsync(id, cancellationToken)!;

    public Task<IQueryable<ThingClass>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.Query());
    }
}
