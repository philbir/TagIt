using System.Reflection.Metadata;
using TagIt.Store;

namespace TagIt.GraphQL;

public class ThingClassByIdDataLoader
    : BatchDataLoader<Guid, ThingClass?>
{
    private readonly IThingClassStore _store;

    public ThingClassByIdDataLoader(
        IThingClassStore store,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _store = store;
    }

    protected override async Task<IReadOnlyDictionary<Guid, ThingClass?>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        IEnumerable<ThingClass> classes =
            await _store.GetManyAsync(keys, cancellationToken);

        return classes.ToDictionary(x => x.Id)!;
    }
}


