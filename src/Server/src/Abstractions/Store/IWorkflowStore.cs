namespace TagIt.Store;

public interface IWorkflowStore
{
    public IQueryable<Workflow> Query();

    Task<IReadOnlyList<Workflow>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken);

    Task<Workflow> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<Workflow> InsertAsync(Workflow entity, CancellationToken cancellationToken);

    Task<Workflow> UpdateAsync(Workflow entity, CancellationToken cancellationToken);
    Task UpdateStepAsync(Guid workflowId,  WorkflowStep step, CancellationToken cancellationToken);
}
