using TagIt.Store;

namespace TagIt.Connectors;

public class ConnectorDefinitionService : IConnectorDefinitionService
{
    private readonly IConnectorStore _store;

    public ConnectorDefinitionService(IConnectorStore store)
    {
        _store = store;
    }

    public Task<IQueryable<ConnectorDefintion>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.Query());
    }

    public Task<ConnectorDefintion> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _store.GetByIdAsync(id, cancellationToken)!;
    }
}
