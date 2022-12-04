namespace TagIt.Store.Mongo;

public class ReceiverStore : Store<Receiver>, IReceiverStore
{
    public ReceiverStore(ITagIdDbContext dbContext)
    : base(dbContext)
    {
    }

    IQueryable<Receiver> IReceiverStore.Query()
    {
        return Query;
    }
}
