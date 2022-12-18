using IdentityModel.Client;
using MassTransit;
using Serilog;
using TagIt.Messaging;
using TagIt.Store;

namespace TagIt;

public class WorkflowEngine : IWorkflowEngine
{
    private readonly IWorkflowStore _store;
    private readonly IMessagePublisher _messagePublisher;
    private readonly IEnumerable<WorkflowTemplate> _templates;
    private readonly IEnumerable<IWorkflowStep> _steps;

    public WorkflowEngine(
        IWorkflowStore store,
        IMessagePublisher messagePublisher,
        IEnumerable<WorkflowTemplate> templates,
        IEnumerable<IWorkflowStep> steps)
    {
        _store = store;
        _messagePublisher = messagePublisher;
        _templates = templates;
        _steps = steps;
    }

    public async Task<Guid> StartWorkflow(
        string name,
        IWorkflowData data,
        CancellationToken cancellationToken)
    {
        var template = _templates.Single(x => x.Name == name);

         return await StartWorkflow(template, data, cancellationToken);
    }

    public async Task<Guid> StartWorkflow(
        WorkflowTemplate template,
        IWorkflowData data,
        CancellationToken cancellationToken)
    {
        var workflow = new Workflow
        {
            Id = Guid.NewGuid(),
            Template = template.Name,
            State = WorkflowState.Started,
            StatedAt = DateTime.UtcNow,
            Data = data,
            Steps = template.Steps.Select(x =>
                    new WorkflowStep { Id = Guid.NewGuid(), Name = x, State = WorkflowStepState.Created })
                .ToList(),
        };

        await _store.InsertAsync(workflow, cancellationToken);

        await _messagePublisher.PublishAsync(
            new WorkflowChangedMessage(workflow.Id, WorkflowChangedActivities.Created),
            cancellationToken);

        return workflow.Id;
    }

    public async Task HandleWorkflowChanged(WorkflowChangedMessage message, CancellationToken cancellationToken)
    {
        var workflow = await GetByIdAsync(message.WorkflowId, cancellationToken);
        Log.Information("WorkflowChanged: {Id}", message.WorkflowId);

        //Get Next open step
        var step = workflow.Steps.FirstOrDefault(x => x.State == WorkflowStepState.Created);

        var context = new WorkflowExecutionContext(workflow, cancellationToken);

        if (step is { })
        {
            step.StartedAt = DateTime.UtcNow;
            step.State = WorkflowStepState.Running;

            await _store.UpdateStepAsync(workflow.Id, step, cancellationToken);

            try
            {
                await ExecuteStepAsync(step, context, cancellationToken);

                step.State = WorkflowStepState.Completed;
                step.CompletedAt = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                step.State = WorkflowStepState.Failed;
                step.Message = ex.Message;
            }

            await _store.UpdateStepAsync(workflow.Id, step, cancellationToken);

            if (step.State == WorkflowStepState.Completed)
            {
                await _messagePublisher.PublishAsync(
                    new WorkflowChangedMessage(workflow.Id, WorkflowChangedActivities.StepCompleted)
                    {
                        StepId = step.Id
                    },
                    cancellationToken);
            }
        }
        else
        {
            // Workflow is completed when all steps completed
            if (workflow.Steps.All(x => x.State == WorkflowStepState.Completed))
            {
                workflow.State = WorkflowState.Completed;
                workflow.CompletedAt = DateTime.UtcNow;

                await _store.UpdateAsync(workflow, cancellationToken);
            }
        }
    }

    private async Task ExecuteStepAsync(
        WorkflowStep step,
        WorkflowExecutionContext context,
        CancellationToken cancellationToken)
    {
        var task = _steps.Single(x => x.Name == step.Name);
        await task.ExecuteAsync(step, context);
    }

    public async Task<Workflow> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _store.GetByIdAsync(id, cancellationToken);
    }
}

public class WorkflowChangedConsumer : IConsumer<WorkflowChangedMessage>
{
    private readonly IWorkflowEngine _engine;

    public WorkflowChangedConsumer(IWorkflowEngine engine)
    {
        _engine = engine;
    }

    public async Task Consume(ConsumeContext<WorkflowChangedMessage> context)
    {
        await _engine.HandleWorkflowChanged(context.Message, context.CancellationToken);
    }
}

public static class WorkflowExecutionContextExtensions
{
    public static TData Data<TData>(this WorkflowExecutionContext context)
    {
        var data = (TData)context.Workflow.Data;

        return data;
    }
}

public static class WorkflowChangedActivities
{
    public static readonly string Created = "Created";

    public static readonly string StepCompleted = "StepCompleted";
}

public static class WorkflowStepNames
{
    public static readonly string CreateThmumbnails = "CreateThmumbnails";
    public static readonly string OCR = "OCR";
    public static readonly string ThingContentExtraction = "ThingContentExtracation";
    public static readonly string ThingDetectProperties = "ThingDetectProperties";
    public static readonly string ThingPostProcessingCompleted = "ThingPostProcessingCompleted";
}

