using Quartz;
using Serilog;
using TagIt.Connectors;
using TagIt.Messaging;

namespace TagIt.Jobs;

public class HarvesterJob : IJob
{
    private readonly IJobManager _jobManager;
    private readonly IConnectorFactory _connectorFactory;
    private readonly IMessagePublisher _messagePublisher;

    public HarvesterJob(
        IJobManager jobManager,
        IConnectorFactory connectorFactory,
        IMessagePublisher messagePublisher)
    {
        _jobManager = jobManager;
        _connectorFactory = connectorFactory;
        _messagePublisher = messagePublisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Guid id = Guid.Parse(
            context.JobDetail.JobDataMap["JobDefintionId"]!.ToString());

        Log.Information("Executing harvester ({Id})", id);

        JobRun? run = await _jobManager.CreateRunAsync(id, context.CancellationToken);

        if (run is null)
        {
            // There is allready a job running
            Log.Warning("There is allready a run for this job in progress ({Id})", id);
            return;
        }

        try
        {
            JobDefintion jobDefintion = run.JobDefintion;

            IConnector connector = await _connectorFactory.CreateAsync(
                jobDefintion.SourceConnectorId,
                context.CancellationToken);

            GetItemsResult items = await connector.GetItemsAsync(
                new GetItemsFilter
                {
                    IncludeChildren = true,
                    Filter = jobDefintion.Filter
                }, context.CancellationToken);

            foreach (ConnectorItem? item in items.Items)
            {
                await _messagePublisher.PublishAsync(
                    new NewConnectorItemMessage(item, jobDefintion.Action),
                    context.CancellationToken);

                run.Messages.Add($"Process Item: {item.Location}");
            }

            await _jobManager.CompleteRunAsync(run, context.CancellationToken);
        }
        catch (Exception ex)
        {
            await _jobManager.FailRunAsync(run, ex, context.CancellationToken);
        }
    }
}
