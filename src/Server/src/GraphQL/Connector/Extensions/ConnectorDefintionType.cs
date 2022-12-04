namespace TagIt.GraphQL;

public class ConnectorDefintionType : ObjectType<ConnectorDefintion>
{
    protected override void Configure(IObjectTypeDescriptor<ConnectorDefintion> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
        descriptor.Field(x => x.CredentialId).Ignore();

        descriptor.Field("credential")
            .ResolveWith<Resolvers>(x => x.GetCredentialAsync(default!, default!, default!));
    }

    class Resolvers
    {
        internal async Task<Credential?> GetCredentialAsync(
            [DataLoader] CredentialByIdDataLoader dataLoader,
            [Parent] ConnectorDefintion connector,
            CancellationToken cancellationToken)
        {
            if (connector.CredentialId is { })
            {
                return await dataLoader.LoadAsync(connector.CredentialId.Value, cancellationToken);
            }

            return null;
        }
    }
}

public class SourceActionType : ObjectType<SourceAction>
{
    protected override void Configure(IObjectTypeDescriptor<SourceAction> descriptor)
    {
        descriptor.Field(x => x.NewConnectorId).ID(nameof(ConnectorDefintion));
    }
}

