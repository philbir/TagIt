namespace TagIt.Jobs;

public interface IJobDefintionService
{
    Task<JobDefintion> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<JobDefintion>> Query(CancellationToken cancellationToken);
    Task<JobDefintion> UpdateAsync(JobDefintion jobDefintion, CancellationToken cancellationToken);
}