namespace TagIt.Store.Mongo;

public class JobDefintionStore : Store<JobDefintion>, IJobDefintionStore
{
    public JobDefintionStore(ITagIdDbContext dbContext)
        : base(dbContext)
    {
    }

    IQueryable<JobDefintion> IJobDefintionStore.Query()
    {
        return Query;
    }
}
