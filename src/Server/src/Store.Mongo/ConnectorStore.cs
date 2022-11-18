namespace TagIt.Store.Mongo;

public class ConnectorStore : Store<ConnectorDefintion>, IConnectorStore
{
    public ConnectorStore(ITagIdDbContext dbContext)
        : base(dbContext)
    {
    }

    IQueryable<ConnectorDefintion> IConnectorStore.Query()
    {
        return Query;
    }
}
