namespace TagIt;

public class WorkflowWorker : BackgroundService
{
    private readonly IWorkflowEngine _engine;

    public WorkflowWorker(IWorkflowEngine engine)
    {
        _engine = engine;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var template = new WorkflowTemplate { Name = "Test1", Steps = new List<string> { "StepA", "StepB" } };

        var wf = await _engine.StartWorkflow(template, new ThingWorkflowData(Guid.NewGuid()), stoppingToken);
    }
}

public class StepA : IWorkflowStep
{
    public Task<WorkflowStepResult> ExecuteAsync(WorkflowStep step, WorkflowExecutionContext context)
    {
        Console.WriteLine("A");

        return Task.FromResult(new WorkflowStepResult());
    }

    public string Name => "StepA";
}

public class StepB : IWorkflowStep
{
    public Task<WorkflowStepResult> ExecuteAsync(WorkflowStep step, WorkflowExecutionContext context)
    {
        Console.WriteLine("B");

        return Task.FromResult(new WorkflowStepResult());
    }

    public string Name => "StepB";
}
