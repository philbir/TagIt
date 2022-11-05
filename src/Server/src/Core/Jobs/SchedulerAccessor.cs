using Quartz;
using Quartz.Impl;

namespace TagIt.Jobs;

public class SchedulerAccessor
{
    private readonly StdSchedulerFactory _factory;

    public SchedulerAccessor(StdSchedulerFactory factory)
    {
        _factory = factory;
    }

    public IScheduler GetScheduler() => _factory.GetScheduler().GetAwaiter().GetResult();
}
