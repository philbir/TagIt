namespace TagIt.Processing;

public class ThingPostProcessingCompletedStep : IWorkflowStep
{
    private readonly IThingService _thingService;

    public ThingPostProcessingCompletedStep(IThingService thingService)
    {
        _thingService = thingService;
    }

    public async Task<WorkflowStepResult> ExecuteAsync(WorkflowStep step, WorkflowExecutionContext context)
    {
        await _thingService.UpdateStateAsync(
            context.Data<ThingWorkflowData>().Id,
            ThingState.New,
            context.CancellationToken);

        return new WorkflowStepResult();
    }

    public string Name => WorkflowStepNames.ThingPostProcessingCompleted;
}
