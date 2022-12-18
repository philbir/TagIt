using TagIt.Store;

namespace TagIt;

public class ThingTypeService : IThingTypeService
{
    private readonly IEntityManager<ThingType> _entityManager;
    private readonly IThingTypeStore _store;
    private readonly IContentDetectorService _contentDetectorService;

    public ThingTypeService(
        IEntityManager<ThingType> entityManager,
        IThingTypeStore store,
        IContentDetectorService contentDetectorService)
    {
        _entityManager = entityManager;
        _store = store;
        _contentDetectorService = contentDetectorService;
    }

    public Task<ThingType> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _store.GetByIdAsync(id, cancellationToken)!;

    public Task<IQueryable<ThingType>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.Query());
    }

    public async Task<IReadOnlyList<DetectResult<ThingType>>> DetectFromContentAsync(
        IThingContentAccessor content,
        CancellationToken cancellationToken)
    {
        var types = (await Query(cancellationToken)).ToList();

        return _contentDetectorService.Detect(types, content);
    }
}
