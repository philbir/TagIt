using MongoDB.Driver;

namespace TagIt.Store.Mongo;

public class JobRunStore : Store<JobRun>, IJobRunStore
{
    public JobRunStore(ITagIdDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<JobRun> GetLatestRunningAsync(
        Guid defintionId,
        CancellationToken cancellationToken)
    {
        return Collection.AsQueryable()
            .Where(x => x.JobDefintionId == defintionId)
            .Where(x => x.State == JobRunState.Running)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
