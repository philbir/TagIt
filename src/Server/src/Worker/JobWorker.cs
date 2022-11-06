using TagIt.Jobs;

namespace TagIt;

public class JobWorker : BackgroundService
{
    private readonly IJobManager _jobManager;

    public JobWorker(IJobManager jobManager)
    {
        _jobManager = jobManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _jobManager.StartAndScheduleAsync(stoppingToken);
    }
}
