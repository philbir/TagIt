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

    public Task<JobDefintion> UpdateAsync(
        JobDefintion jobDefintion,
        CancellationToken cancellationToken)
    {
        if (jobDefintion.RunMode == JobRunMode.Watch)
        {
            jobDefintion.Schedule = null;
        }
        else if (jobDefintion.Schedule is { } schedule)
        {
            if (schedule.Type == JobScheduleType.Interval)
            {
                schedule.CronExpression = null;
            }
            else
            {
                schedule.Intervall = null;
            }
        }

        if (jobDefintion.Action?.Source?.Mode == SourceActionMode.Delete)
        {
            jobDefintion.Action.Source.NewLocation = null;
            jobDefintion.Action.Source.NewConnectorId = null;
        }

        return _store.UpdateAsync(jobDefintion, cancellationToken);
    }
}
