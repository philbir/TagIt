namespace TagIt.GraphQL;

[ExtendObjectType(typeof(ConnectorDefintion))]
public class ConnectorExtensions
{
    public async Task<Credential?> GetCredentialAsync(
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




