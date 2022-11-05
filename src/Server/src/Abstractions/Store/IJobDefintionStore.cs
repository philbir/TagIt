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
}
