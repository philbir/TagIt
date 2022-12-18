#nullable disable
using TagIt.Store;

namespace TagIt;

public class Workflow : IEntity
{
    public Guid Id { get; set; }

    public string Template { get; set; }

    public IWorkflowData Data { get; set; }

    public WorkflowState State { get; set; }

    public DateTime StatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public List<WorkflowStep> Steps { get; set; } = new List<WorkflowStep>();
}

public class WorkflowStep
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public WorkflowStepState State { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? Message { get; set; }
}

public enum WorkflowStepState
{
    Created,
    Running,
    Completed,
    Failed
}

public enum WorkflowState
{
    Started,
    Running,
    Completed,
    Failed
}

public interface IWorkflowData
{

}

public interface IWorkflowEngine
{
    Task<Guid> StartWorkflow(
        WorkflowTemplate template,
        IWorkflowData data,
        CancellationToken cancellationToken);

    Task HandleWorkflowChanged(WorkflowChangedMessage message, CancellationToken cancellationToken);
    Task<Workflow> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Guid> StartWorkflow(
        string name,
        IWorkflowData data,
        CancellationToken cancellationToken);
}

public interface IWorkflowStep
{
    Task<WorkflowStepResult> ExecuteAsync(WorkflowStep step, WorkflowExecutionContext context);

    string Name { get; }
}

public class WorkflowStepResult
{

}

public class WorkflowExecutionContext
{
    public WorkflowExecutionContext(Workflow workflow, CancellationToken cancellationToken)
    {
        Workflow = workflow;
        CancellationToken = cancellationToken;
    }

    public Workflow Workflow { get; }
    public CancellationToken CancellationToken { get; }
}

public class WorkflowTemplate
{
    public string Name { get; set; }

    public List<string> Steps { get; set; }
}

public class ThingWorkflowData : IWorkflowData
{
    public ThingWorkflowData(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}


public class WorkflowChangedMessage
{
    public WorkflowChangedMessage()
    {

    }

    public WorkflowChangedMessage(Guid workflowId, string activity)
    {
        WorkflowId = workflowId;
        Activity = activity;
    }

    public Guid WorkflowId { get; set; }

    public Guid? StepId { get; set; }

    public string Activity { get; set; }
}
