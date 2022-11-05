using System.Data;
using System.Diagnostics;
using Quartz;
using Quartz.Spi;
using TagIt.Connectors;
using TagIt.Store;

namespace TagIt.Jobs;

public class JobManager : IJobManager
{
    private readonly IJobDefintionStore _jobDefintionStore;
    private readonly IJobRunStore _jobRunStore;
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IJobFactory _jobFactory;
    private readonly IConnectorFactory _connectorFactory;

    public JobManager(
        IJobDefintionStore jobDefintionStore,
        IJobRunStore jobRunStore,
        ISchedulerFactory schedulerFactory,
        IJobFactory jobFactory,
        IConnectorFactory connectorFactory)
    {
        _jobDefintionStore = jobDefintionStore;
        _jobRunStore = jobRunStore;
        _schedulerFactory = schedulerFactory;
        _jobFactory = jobFactory;
        _connectorFactory = connectorFactory;
    }

    public async Task StartAndScheduleAsync(CancellationToken cancellationToken)
    {
        IEnumerable<JobDefintion> jobs = (await _jobDefintionStore.GetAllAsync(cancellationToken))
            .Where(x => x.Enabled);

        await ScheduleJobsAsync(
            jobs.Where(x => x.RunMode == JobRunMode.Harvest),
            cancellationToken);

        await StartWatchersAsync(
            jobs.Where(x => x.RunMode == JobRunMode.Watch),
            cancellationToken);
    }

    private async Task StartWatchersAsync(
        IEnumerable<JobDefintion> jobs,
        CancellationToken cancellationToken)
    {
        foreach (JobDefintion job in jobs)
        {
            IConnector connector = await _connectorFactory
                .CreateAsync(job.SourceConnectorId, cancellationToken);

            await connector.StartWatchingAsync(job, new WatchOptions(), cancellationToken);
        }
    }

    private async Task ScheduleJobsAsync(
        IEnumerable<JobDefintion> jobs,
        CancellationToken cancellationToken)
    {
        IScheduler scheduler = await _schedulerFactory
            .GetScheduler(cancellationToken);

        scheduler.JobFactory = _jobFactory;

        foreach (JobDefintion job in jobs)
        {
            if (job.CronSchedule is { })
            {
                IJobDetail jobDetail = JobBuilder
                    .Create(typeof(HarvesterJob))
                    .UsingJobData("JobDefintionId", job.Id)
                    .WithIdentity(job.Name)
                    .Build();

                TriggerBuilder triggerBuilder = TriggerBuilder
                    .Create()
                    .WithIdentity(job.Name);

                triggerBuilder.WithSimpleSchedule(s => s
                    .WithInterval(TimeSpan.FromMinutes(30))
                    .RepeatForever());

                await scheduler.ScheduleJob(jobDetail, triggerBuilder.Build(), cancellationToken);
            }
        }

        await scheduler.Start();
    }

    public async Task<JobRun?> CreateRunAsync(
        Guid jobDefintionId,
        CancellationToken cancellationToken)
    {
        //Allready running
        JobRun runningJob = await _jobRunStore.GetLatestRunningAsync(jobDefintionId, cancellationToken);

        if (runningJob is { })
        {
            if (!Debugger.IsAttached)
            {
                return null;
            }
        }

        var run = new JobRun
        {
            Id = Guid.NewGuid(),
            StartedAt = DateTime.UtcNow,
            State = JobRunState.Running,
            JobDefintionId = jobDefintionId,
        };

        await _jobRunStore.SaveAsync(run, cancellationToken);

        run.JobDefintion = await _jobDefintionStore.GetByIdAsync(jobDefintionId, cancellationToken);

        return run;
    }

    public Task<JobDefintion> GetDefintionAsync(Guid id, CancellationToken cancellationToken)
        => _jobDefintionStore.GetByIdAsync(id, cancellationToken);

    public async Task CompleteRunAsync(
        JobRun run,
        CancellationToken cancellationToken)
    {
        run.CompletedAt = DateTime.UtcNow;
        run.State = JobRunState.Completed;

        await _jobRunStore.SaveAsync(run, cancellationToken);
    }

    public async Task FailRunAsync(
        JobRun run,
        Exception ex,
        CancellationToken cancellationToken)
    {
        run.CompletedAt = DateTime.UtcNow;
        run.State = JobRunState.Failed;
        run.Messages = new List<string>(run.Messages)
        {
            ex.Message
        };

        await _jobRunStore.SaveAsync(run, cancellationToken);
    }
}
