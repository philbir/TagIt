using MassTransit;
using Serilog;
using TagIt.Processing;

namespace TagIt.Messaging;

public class ThingAddedConsumer : IConsumer<ThingAddedMessage>
{
    private readonly IWorkflowEngine _workflowEngine;
    private readonly IThingService _thingService;

    public ThingAddedConsumer(IWorkflowEngine workflowEngine, IThingService thingService)
    {
        _workflowEngine = workflowEngine;
        _thingService = thingService;
    }

    public async Task Consume(ConsumeContext<ThingAddedMessage> context)
    {
        Log.Information("Consume ThingAdded: {Id}", context.Message.Id);

        await _thingService.UpdateStateAsync(
            context.Message.Id,
            ThingState.Processing,
            context.CancellationToken);

        await _workflowEngine.StartWorkflow(
            "ThingPostProcess",
            new ThingWorkflowData(context.Message.Id),
            context.CancellationToken);
    }
}
