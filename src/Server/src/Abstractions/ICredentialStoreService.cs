namespace TagIt;

public interface ICredentialStoreService
{
    Task<Credential> AddAsync(Credential credential, CancellationToken cancellationToken);
    Task<Credential> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<Credential>> Query(CancellationToken cancellationToken);
    Task<Credential> RegisterOAuthClientAsync(string name, OAuthClient client, CancellationToken cancellationToken);
    Task<Credential> UpdateAsync(Credential credential, CancellationToken cancellationToken);
}
