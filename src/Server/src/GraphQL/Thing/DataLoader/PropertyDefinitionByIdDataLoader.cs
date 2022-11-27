using TagIt.Store;

namespace TagIt.GraphQL;

public class PropertyDefinitionByIdDataLoader
    : BatchDataLoader<Guid, PropertyDefinition?>
{
    private readonly IPropertyDefinitionStore _store;

    public PropertyDefinitionByIdDataLoader(
        IPropertyDefinitionStore store,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _store = store;
    }

    protected override async Task<IReadOnlyDictionary<Guid, PropertyDefinition?>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        IEnumerable<PropertyDefinition> definitions =
            await _store.GetManyAsync(keys, cancellationToken);

        return definitions.ToDictionary(x => x.Id)!;
    }
}


