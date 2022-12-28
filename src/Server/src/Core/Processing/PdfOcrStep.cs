namespace TagIt.Processing;

public class OCRStep : IWorkflowStep
{
    public Task<WorkflowStepResult> ExecuteAsync(WorkflowStep step, WorkflowExecutionContext context)
    {

        return Task.FromResult(new WorkflowStepResult());
    }

    public string Name => WorkflowStepNames.OCR;
}
