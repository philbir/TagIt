namespace TagIt.GraphQL;

[QueryType]
public class CredentialQueries
{
    [UsePaging]
    [UseFiltering]
    public Task<IQueryable<Credential>> GetCredentialsAsync(
        [Service] ICredentialStoreService service,
        CancellationToken cancellationToken)
            => service.Query(cancellationToken);

    [NodeResolver]
    public Task<Credential> GetCredentialsByIdAsync(
        [ID(nameof(Credential))] Guid id,
        [Service] ICredentialStoreService service,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(id, cancellationToken);
    }
}
