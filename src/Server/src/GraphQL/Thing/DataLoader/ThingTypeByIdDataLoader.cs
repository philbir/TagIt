using MassTransit.Initializers;
using TagIt.Store;

namespace TagIt.GraphQL;

public class ThingTypeByIdDataLoader
    : BatchDataLoader<Guid, ThingType?>
{
    private readonly IThingTypeStore _store;

    public ThingTypeByIdDataLoader(
        IThingTypeStore store,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _store = store;
    }

    protected override async Task<IReadOnlyDictionary<Guid, ThingType?>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        IEnumerable<ThingType> types =
            await _store.GetManyAsync(keys, cancellationToken);

        return types.ToDictionary(x => x.Id)!;
    }
}
