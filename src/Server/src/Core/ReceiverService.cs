using TagIt.Store;

namespace TagIt;

public class ReceiverService : IReceiverService
{
    private readonly IReceiverStore _store;
    private readonly IContentDetectorService _detectorService;

    public ReceiverService(IReceiverStore store, IContentDetectorService detectorService)
    {
        _store = store;
        _detectorService = detectorService;
    }

    public Task<IQueryable<Receiver>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.Query());
    }

    public Task<Receiver> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _store.GetByIdAsync(id, cancellationToken)!;
    }

    public Task<IReadOnlyList<Receiver>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken)
         => _store.GetManyAsync(ids, cancellationToken);

    public async Task<IReadOnlyList<DetectResult<Receiver>>> DetectFromConteÈntAsync(
        IThingContentAccessor content,
        CancellationToken cancellationToken)
    {
        var all = await _store.GetAllAsync(cancellationToken);

        return _detectorService.Detect(all, content);
    }
}
