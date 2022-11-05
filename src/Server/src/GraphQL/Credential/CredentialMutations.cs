
namespace TagIt.GraphQL;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class CredentialMutations
{
    public async Task<Credential> AddOAuthCredentialClientAsync(
        [Service] ICredentialStoreService service,
        string name,
        OAuthClient client,
        CancellationToken cancellationToken)
    {
        return await service.RegisterOAuthClientAsync(
            name,
            client,
            cancellationToken);
    }
}
