using System.ComponentModel;
using TagIt.Jobs;

namespace TagIt.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class JobQueries
{
    [UsePaging]
    [UseFiltering]
    public Task<IQueryable<JobDefintion>> GetJobDefintionsAsync(
        [Service] IJobDefintionService service,
        CancellationToken cancellationToken)
    {
        return service.Query(cancellationToken);
    }

    public Task<JobDefintion> GetJobDefinitionByIdAsync(
        [ID(nameof(JobDefintion))] Guid id,
        [Service] IJobDefintionService service,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(id, cancellationToken);
    }
}


[ExtendObjectType(OperationTypeNames.Mutation)]
public class JobMutations
{
    public async Task<JobDefintion> UpdateJobDefinitionAsync(
        [Service] IJobDefintionService service,
        UpdateJobDefinitionInput input,
        CancellationToken cancellationToken)
    {
        var defintion = new JobDefintion
        {
            Id = input.Id,
            Name = input.Name,
            Enabled = input.Enabled,
            Filter = input.Filter,
            RunMode = input.RunMode,
            SourceConnectorId = input.SourceConnectorId,
            Action = input.Action,
            Schedule = input.Schedule
        };

        return await service.UpdateAsync(defintion, cancellationToken);
    }
}

public class UpdateJobDefinitionInput
{
    [ID(nameof(JobDefintion))]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public JobRunMode RunMode { get; set; }

    public string? Filter { get; set; }

    [ID(nameof(ConnectorDefintion))]
    public Guid SourceConnectorId { get; set; }

    public JobAction Action { get; set; }

    public JobSchedule? Schedule { get; set; }

    public bool Enabled { get; set; }
}
