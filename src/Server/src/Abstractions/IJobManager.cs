namespace TagIt.Jobs;

public interface IJobManager
{
    Task CompleteRunAsync(JobRun run, CancellationToken cancellationToken);
    Task<JobRun?> CreateRunAsync(Guid jobDefintionId, CancellationToken cancellationToken);
    Task FailRunAsync(JobRun run, Exception ex, CancellationToken cancellationToken);
    Task<JobDefintion> GetDefintionAsync(Guid id, CancellationToken cancellationToken);
    Task StartAndScheduleAsync(CancellationToken cancellationToken);
}
