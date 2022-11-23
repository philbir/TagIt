using TagIt.Jobs;

namespace TagIt.GraphQL;

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
