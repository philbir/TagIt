namespace TagIt.Processing;

public class ThingDetectPropertiesStep : IWorkflowStep
{
    private readonly IThingPostProcessingService _postProcessingService;

    public ThingDetectPropertiesStep(IThingPostProcessingService postProcessingService)
    {
        _postProcessingService = postProcessingService;
    }

    public async Task<WorkflowStepResult> ExecuteAsync(WorkflowStep step, WorkflowExecutionContext context)
    {
        await _postProcessingService.DetectPropertiesAsync(
            context.Data<ThingWorkflowData>().Id,
            context.CancellationToken);

        return new WorkflowStepResult();
    }

    public string Name => WorkflowStepNames.ThingDetectProperties;
}
