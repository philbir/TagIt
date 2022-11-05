namespace TagIt.Store.Mongo;

public class ClientAuthStateStore : Store<ClientAuthState>, IClientAuthStateStore
{
    public ClientAuthStateStore(ITagIdDbContext dbContext)
        : base(dbContext)
    {
    }
}
