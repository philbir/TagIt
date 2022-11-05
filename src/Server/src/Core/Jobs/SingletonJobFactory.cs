using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace TagIt.Jobs;

public class SingletonJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;
    public SingletonJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        IEnumerable<IJob> jobs = _serviceProvider.GetRequiredService<IEnumerable<IJob>>();

        return jobs.Single(x => x.GetType() == bundle.JobDetail.JobType);
    }

    public void ReturnJob(IJob job)
    {
        // Method intentionally left empty.
    }
}
