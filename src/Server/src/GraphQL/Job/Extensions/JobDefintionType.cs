using TagIt.GraphQL.Connector.DataLoader;

namespace TagIt.GraphQL;

public class JobDefintionType : ObjectType<JobDefintion>
{
    protected override void Configure(IObjectTypeDescriptor<JobDefintion> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
        descriptor.Field(x => x.SourceConnectorId).Ignore();

        descriptor.Field("sourceConnector")
            .ResolveWith<Resolvers>(x => x.GetSourceConnectorAsync(default!, default!, default!));
    }

    class Resolvers
    {
        internal Task<ConnectorDefintion> GetSourceConnectorAsync(
            [DataLoader] ConnectorDefinitionByIdDataLoader dataLoader,
            [Parent] JobDefintion job,
            CancellationToken cancellationToken)
        {
            return dataLoader.LoadAsync(job.SourceConnectorId, cancellationToken)!;
        }
    }
}
