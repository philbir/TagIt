namespace TagIt.Store.Mongo;

public class ThingClassStore : Store<ThingClass>, IThingClassStore
{
    public ThingClassStore(ITagIdDbContext dbContext)
        : base(dbContext)
    {
    }
}
