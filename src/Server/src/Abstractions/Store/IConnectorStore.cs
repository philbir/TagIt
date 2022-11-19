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

    Task<IReadOnlyList<ConnectorDefintion>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken);

    Task<ConnectorDefintion> InsertAsync(ConnectorDefintion entity, CancellationToken cancellationToken);

    Task<ConnectorDefintion> UpdateAsync(ConnectorDefintion entity, CancellationToken cancellationToken);

    public IQueryable<ConnectorDefintion> Query();
}
