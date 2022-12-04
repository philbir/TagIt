using TagIt.Store;

namespace TagIt;

public class ThingTypeService : IThingTypeService
{
    private readonly IEntityManager<ThingType> _entityManager;
    private readonly IThingTypeStore _store;

    public ThingTypeService(
        IEntityManager<ThingType> entityManager,
        IThingTypeStore store)
    {
        _entityManager = entityManager;
        _store = store;
    }

    public Task<ThingType> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _store.GetByIdAsync(id, cancellationToken)!;

    public Task<IQueryable<ThingType>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.Query());
    }
}
