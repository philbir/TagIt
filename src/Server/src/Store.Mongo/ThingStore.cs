
using MongoDB.Driver;

namespace TagIt.Store.Mongo;

public class ThingStore : Store<Thing>, IThingStore
{
    public ThingStore(ITagIdDbContext dbContext) : base(dbContext)
    {
    }

    IQueryable<Thing> IThingStore.Query()
    {
        return Query;
    }

    public async Task UpdateThumbnailsAsync(
        Guid id,
        List<ThingThumbnail> thumbails,
        CancellationToken cancellationToken)
    {
        await Collection.UpdateOneAsync(
            x => x.Id == id,
            Builders<Thing>.Update.Set(f => f.Thumbnails, thumbails),
            new UpdateOptions(),
            cancellationToken);
    }
}
