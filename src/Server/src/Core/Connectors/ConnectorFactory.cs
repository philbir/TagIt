using TagIt.Store;

namespace TagIt.Connectors;

public class ConnectorFactory : IConnectorFactory
{
    private readonly IConnectorStore _connectorStore;
    private readonly IEnumerable<IConnectionManager> _managers;

    public ConnectorFactory(
        IConnectorStore connectorStore,
        IEnumerable<IConnectionManager> managers)
    {
        _connectorStore = connectorStore;
        _managers = managers;
    }

    private IReadOnlyList<ConnectorDefintion>? _connectorDefintions;

    public async Task<IConnector> CreateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        if ( _connectorDefintions == null )
        {
            _connectorDefintions = await _connectorStore.GetAllAsync(
                cancellationToken);
        }

        ConnectorDefintion definition = _connectorDefintions
            .Single(x => x.Id == id);

        IConnectionManager? manager = _managers.SingleOrDefault(
            x => x.ManagedTypes.Contains(definition.Type));

        if ( manager is null)
        {
            throw new InvalidOperationException(
                $"No manager found for type: {definition.Type}");
        }

        return await manager.CreateAsync(definition, cancellationToken);
    }
}
