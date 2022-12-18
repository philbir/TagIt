using TagIt.Store;

namespace TagIt;

public class ThingClassService : IThingClassService
{
    private readonly IEntityManager<ThingType> _entityManager;
    private readonly IThingClassStore _store;
    private readonly IThingTypeStore _thingTypeStore;
    private readonly IContentDetectorService _detectorService;

    public ThingClassService(
        IEntityManager<ThingType> entityManager,
        IThingClassStore store,
        IThingTypeStore thingTypeStore,
        IContentDetectorService detectorService)
    {
        _entityManager = entityManager;
        _store = store;
        _thingTypeStore = thingTypeStore;
        _detectorService = detectorService;
    }

    public Task<ThingClass> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _store.GetByIdAsync(id, cancellationToken)!;

    public Task<IQueryable<ThingClass>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.Query());
    }

    public async Task<IReadOnlyList<ThingClass>> GetByTypeAsync(Guid typeId, CancellationToken cancellationToken)
    {
        var type = await _thingTypeStore.GetByIdAsync(typeId, cancellationToken);

        if (type.ValidClasses.Any())
        {
            return await GetManyAsync(type.ValidClasses, cancellationToken);
        }

        return Array.Empty<ThingClass>();
    }

    public async Task<IReadOnlyList<DetectResult<ThingClass>>> DetectFromContentAsync(
        Guid typeId,
        IThingContentAccessor content,
        CancellationToken cancellationToken)
    {
        var classes = await GetByTypeAsync(typeId, cancellationToken);

        return _detectorService.Detect(classes, content);
    }

    public Task<IReadOnlyList<ThingClass>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken)
         => _store.GetManyAsync(ids, cancellationToken)!;

}
