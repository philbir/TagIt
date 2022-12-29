namespace TagIt.Processing;

public class CreateThumbnailsStep : IWorkflowStep
{
    private readonly IThumbnailGeneratorService _thumbnailGeneratorService;

    public CreateThumbnailsStep(IThumbnailGeneratorService thumbnailGeneratorService)
    {
        _thumbnailGeneratorService = thumbnailGeneratorService;
    }

    public async Task<WorkflowStepResult> ExecuteAsync(WorkflowStep step, WorkflowExecutionContext context)
    {
        await _thumbnailGeneratorService.UpdateThumbnailsAsync(
            context.Data<ThingWorkflowData>().Id,
            context.CancellationToken);

        return new WorkflowStepResult();
    }

    public string Name => WorkflowStepNames.CreateThumbnails;
}
