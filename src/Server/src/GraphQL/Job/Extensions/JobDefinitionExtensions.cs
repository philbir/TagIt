using TagIt.GraphQL.Connector.DataLoader;

namespace TagIt.GraphQL.Job.Extensions;

[ExtendObjectType(typeof(JobDefintion))]
public class JobDefinitionExtensions
{
    public Task<ConnectorDefintion> GetSourceConnectorAsync(
        [DataLoader] ConnectorDefinitionByIdDataLoader dataLoader,
        [Parent] JobDefintion job,
        CancellationToken cancellationToken)
    {
        return dataLoader.LoadAsync(job.SourceConnectorId, cancellationToken)!;
    }
}
