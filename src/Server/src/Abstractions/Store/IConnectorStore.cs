namespace TagIt.Store;

public interface IConnectorStore
{
    Task<ConnectorDefintion> SaveAsync(
        ConnectorDefintion entity,
        CancellationToken cancellationToken);

    Task<ConnectorDefintion> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<ConnectorDefintion>> GetAllAsync(
        CancellationToken cancellationToken);
}
