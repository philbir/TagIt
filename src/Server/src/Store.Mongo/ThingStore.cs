
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
}


