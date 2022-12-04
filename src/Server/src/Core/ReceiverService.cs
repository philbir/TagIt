using TagIt.Store;

namespace TagIt;

public class ReceiverService : IReceiverService
{
    private readonly IReceiverStore _store;

    public ReceiverService(IReceiverStore store)
    {
        _store = store;
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
}
