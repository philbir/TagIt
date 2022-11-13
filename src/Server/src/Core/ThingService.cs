using TagIt.Store;

namespace TagIt;

public class ThingService : IThingService
{
    private readonly IThingStore _thingStore;

    public ThingService(
        IThingStore thingStore)
    {
        _thingStore = thingStore;
    }

    public Task<IQueryable<Thing>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_thingStore.Query());
    }
    public Task<Thing?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _thingStore.GetByIdAsync(id, cancellationToken)!;
    }
}
