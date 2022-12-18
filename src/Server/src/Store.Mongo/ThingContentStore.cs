using MongoDB.Driver;

namespace TagIt.Store.Mongo;

public class ThingContentStore : Store<ThingContent>, IThingContentStore
{
    public ThingContentStore(ITagIdDbContext dbContext)
        : base(dbContext)
    {
    }

    IQueryable<ThingContent> IThingContentStore.Query()
    {
        return Query;
    }

    public async Task<IReadOnlyList<ThingContent>> GetByThingIdAsync(
        Guid thingId,
        CancellationToken cancellationToken)
    {
        return await Query.Where(x => x.ThingId == thingId)
            .ToListAsync(cancellationToken);
    }
}
