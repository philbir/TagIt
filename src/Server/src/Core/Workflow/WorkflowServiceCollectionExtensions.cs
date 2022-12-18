using Microsoft.Extensions.DependencyInjection;
using TagIt.Configuration;

namespace TagIt;

public static class WorkflowServiceCollectionExtensions
{
    public static WorkflowBuilder AddWorkflow(this ITagItServerBuilder builder)
    {
        builder.Services.AddSingleton<IWorkflowEngine, WorkflowEngine>();

        return new WorkflowBuilder(builder.Services);
    }

    public static WorkflowBuilder RegisterStep<TStep>(this WorkflowBuilder builder)
        where TStep : class, IWorkflowStep
    {
        builder.Services.AddSingleton<IWorkflowStep, TStep>();

        return builder;
    }
}

public class WorkflowBuilder
{
    public WorkflowBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; set; }
}
