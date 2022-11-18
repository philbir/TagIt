namespace TagIt.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class CredentialQueries
{
    public Task<IQueryable<Credential>> GetCredentialsAsync(
        [Service] ICredentialStoreService service,
        CancellationToken cancellationToken)
            => service.Query(cancellationToken);
}


