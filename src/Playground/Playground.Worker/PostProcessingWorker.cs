namespace TagIt;

public class PostProcessingWorker : BackgroundService
{
    private readonly IWorkflowEngine _engine;
    private readonly IThingService _service;

    public PostProcessingWorker(IWorkflowEngine engine, IThingService service)
    {
        _engine = engine;
        _service = service;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var things = await _service.Query(stoppingToken);

        var template = new WorkflowTemplate()
        {
            Name = "ThingPostProcess", Steps = new List<string> { WorkflowStepNames.PdfOcr }
        };

        foreach (Thing thing in things.Skip(1).Take(10))
        {
            await _engine.StartWorkflow(
                template,
                new ThingWorkflowData(thing.Id),
                stoppingToken);
        }
    }
}
