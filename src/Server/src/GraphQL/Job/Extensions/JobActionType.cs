using TagIt.GraphQL.Connector.DataLoader;

namespace TagIt.GraphQL;

public class JobActionType : ObjectType<JobAction>
{
    protected override void Configure(IObjectTypeDescriptor<JobAction> descriptor)
    {
        descriptor.Field(x => x.DestinationConnectorId).Ignore();

        descriptor.Field("destinationConnector")
            .ResolveWith<Resolvers>(x => x.GetDestinationConnectorAsync(default!, default!, default!));
    }

    class Resolvers
    {
        internal async Task<ConnectorDefintion?> GetDestinationConnectorAsync(
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
}
