namespace TagIt.Store;

public interface IJobRunStore
{
    Task<JobRun> GetLatestRunningAsync(Guid defintionId, CancellationToken cancellationToken);
    Task<JobRun> SaveAsync(
        JobRun entity,
        CancellationToken cancellationToken);
}
