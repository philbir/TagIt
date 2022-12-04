namespace TagIt.Store;

public interface IJobDefintionStore
{
    Task<JobDefintion> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<JobDefintion> SaveAsync(
        JobDefintion entity,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<JobDefintion>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<JobDefintion> InsertAsync(JobDefintion entity, CancellationToken cancellationToken);

    Task<JobDefintion> UpdateAsync(JobDefintion entity, CancellationToken cancellationToken);

    public IQueryable<JobDefintion> Query();
}
