using TagIt.Store;

namespace TagIt.GraphQL.Connector.DataLoader;

public class ConnectorDefinitionByIdDataLoader
    : BatchDataLoader<Guid, ConnectorDefintion?>
{
    private readonly IConnectorStore _store;

    public ConnectorDefinitionByIdDataLoader(
        IConnectorStore store,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _store = store;
    }

    protected override async Task<IReadOnlyDictionary<Guid, ConnectorDefintion?>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        IEnumerable<ConnectorDefintion> types =
            await _store.GetManyAsync(keys, cancellationToken);

        return types.ToDictionary(x => x.Id)!;
    }
}

