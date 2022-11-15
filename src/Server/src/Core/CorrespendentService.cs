using TagIt.Store;

namespace TagIt;

public class CorrespondentService : ICorrespondentService
{
    private readonly ICorrespendentStore _store;

    public CorrespondentService(ICorrespendentStore store)
    {
        _store = store;
    }

    public Task<IQueryable<Correspondent>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.Query());
    }

    public Task<Correspondent> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _store.GetByIdAsync(id, cancellationToken)!;
    }

    public Task<Correspondent> AddAsync(
        string name,
        CancellationToken cancellationToken)
    {
        Correspondent correspondent = new ()
        {
            Id = Guid.NewGuid(),
            Name = name,
        };

        return _store.InsertAsync(correspondent, cancellationToken)!;
    }
}
