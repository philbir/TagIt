namespace TagIt.Store.Mongo;

public class CorrespondentStore : Store<Correspondent>, ICorrespendentStore
{
    public CorrespondentStore(ITagIdDbContext dbContext)
    : base(dbContext)
    {
    }

    IQueryable<Correspondent> ICorrespendentStore.Query()
    {
        return Query;
    }
}
