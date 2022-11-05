namespace TagIt.Store;

public interface ICredentialsStore
{
    Task<Credential> InsertAsync(Credential entity, CancellationToken cancellationToken);

    Task<Credential> UpdateAsync(Credential entity, CancellationToken cancellationToken);

    Task<Credential> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    public IQueryable<Credential> Query();
}
