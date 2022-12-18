using TagIt.Store;

namespace TagIt;

public class CorrespondentService : ICorrespondentService
{
    private readonly ICorrespendentStore _store;
    private readonly IContentDetectorService _detectorService;

    public CorrespondentService(
        ICorrespendentStore store,
        IContentDetectorService detectorService)
    {
        _store = store;
        _detectorService = detectorService;
    }

    public Task<IQueryable<Correspondent>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.Query().OrderBy( x => x.Name).AsQueryable());
    }

    public Task<Correspondent> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _store.GetByIdAsync(id, cancellationToken)!;
    }

    public async Task<IReadOnlyList<DetectResult<Correspondent>>> DetectFromContentAsync(
        IThingContentAccessor content,
        CancellationToken cancellationToken)
    {
        var all = await _store.GetAllAsync(cancellationToken);

        return _detectorService.Detect(all, content);
    }

    public Task<Correspondent> AddAsync(
        string name,
        CancellationToken cancellationToken)
    {
        Correspondent correspondent = new ()
        {
            Id = Guid.NewGuid(),
            Name = name,
            DetectRules = new List<DetectRule>
            {
                new()
                {
                    Expression = name,
                    Mode = DetectRuleMode.Regex,
                    Weight = 1
                }
            }
        };

        return _store.InsertAsync(correspondent, cancellationToken)!;
    }

    public Task<IReadOnlyList<Correspondent>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken)
         => _store.GetManyAsync(ids, cancellationToken);
}
