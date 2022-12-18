namespace TagIt.Processing;

public class ThingContentExtractionStep : IWorkflowStep
{
    private readonly IThingService _thingService;
    private readonly IContentExtractionService _contentExtractionService;
    private readonly IThingContentService _contentService;

    public ThingContentExtractionStep(
        IThingService thingService,
        IContentExtractionService contentExtractionService,
        IThingContentService contentService)
    {
        _thingService = thingService;
        _contentExtractionService = contentExtractionService;
        _contentService = contentService;
    }

    public async Task<WorkflowStepResult> ExecuteAsync(WorkflowStep step, WorkflowExecutionContext context)
    {
        Thing thing = await _thingService.GetByIdAsync(
            context.Data<ThingWorkflowData>().Id,
            context.CancellationToken);

        IReadOnlyList<IThingContentData> contents = await _contentExtractionService.ExtractAsync(
            thing,
            context.CancellationToken);

        await _contentService.AddContentAsync(thing.Id, contents, context.CancellationToken);

        return new WorkflowStepResult();
    }

    public string Name => WorkflowStepNames.ThingContentExtraction;
}
