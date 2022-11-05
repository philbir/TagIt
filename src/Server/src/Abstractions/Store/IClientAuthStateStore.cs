namespace TagIt.Store;

public interface IClientAuthStateStore
{
    Task<ClientAuthState> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<ClientAuthState> InsertAsync(
        ClientAuthState clientAuthState,
        CancellationToken cancellationToken);

    Task<long> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
