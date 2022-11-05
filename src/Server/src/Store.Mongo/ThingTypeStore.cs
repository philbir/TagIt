using MongoDB.Driver;

namespace TagIt.Store.Mongo;

public class ThingTypeStore : Store<ThingType>, IThingTypeStore
{
    public ThingTypeStore(ITagIdDbContext dbContext)
        : base(dbContext)
    {
    }
    IQueryable<ThingType> IThingTypeStore.Query()
    {
        return Query;
    }

    public Task<ThingType?> GetByTypeMapAsync(
        string type,
        CancellationToken cancellationToken)
    {
        return Query
            .Where(x => x.TypeMap.Contains(type))
            .FirstOrDefaultAsync(cancellationToken)!;
    }
}
