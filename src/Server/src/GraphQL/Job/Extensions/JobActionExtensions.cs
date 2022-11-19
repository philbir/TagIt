using TagIt.GraphQL.Connector.DataLoader;

namespace TagIt.GraphQL.Job.Extensions;

[ExtendObjectType(typeof(JobAction))]
public class JobActionExtensions
{
    public async Task<ConnectorDefintion?> GetDestinationConnectorAsync(
        [DataLoader] ConnectorDefinitionByIdDataLoader dataLoader,
        [Parent] JobAction jobAction,
        CancellationToken cancellationToken)
    {
        if (jobAction.DestinationConnectorId.HasValue)
        {
            return await dataLoader.LoadAsync(
                jobAction.DestinationConnectorId.Value,
                cancellationToken)!;
        }

        return null;
    }
}
