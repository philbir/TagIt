using TagIt.Store;

namespace TagIt.Jobs;

public class JobDefintionService : IJobDefintionService
{
    private readonly IJobDefintionStore _store;

    public JobDefintionService(IJobDefintionStore store)
    {
        _store = store;
    }

    public Task<IQueryable<JobDefintion>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.Query());
    }

    public Task<JobDefintion> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _store.GetByIdAsync(id, cancellationToken)!;
    }
}
