using TagIt.Store;

namespace TagIt;

public class TagDefinitionService : ITagDefinitionService
{
    private readonly ITagDefinitionStore _store;
    private readonly IContentDetectorService _detectorService;

    public TagDefinitionService(
        ITagDefinitionStore store,
        IContentDetectorService detectorService)
    {
        _store = store;
        _detectorService = detectorService;
    }

    public Task<IQueryable<TagDefinition>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.Query());
    }

    public Task<TagDefinition> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _store.GetByIdAsync(id, cancellationToken)!;
    }

    public async Task<TagDefinition> AddAsync(string name, CancellationToken cancellationToken)
    {
        var tagDefinition = new TagDefinition()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Color = ColorUtils.GetRandomHexColor(),
        };

        return await _store.InsertAsync(tagDefinition, cancellationToken);
    }

    public async Task<IReadOnlyList<DetectResult<TagDefinition>>> DetectFromContentAsync(
        IThingContentAccessor content,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<TagDefinition> all = await _store.GetAllAsync(cancellationToken);

        return _detectorService.Detect(all, content);
    }

    public Task<IReadOnlyList<TagDefinition>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken)
         => _store.GetManyAsync(ids, cancellationToken);
}
