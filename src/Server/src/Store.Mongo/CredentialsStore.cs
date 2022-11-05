
namespace TagIt.Store.Mongo;

public class CredentialsStore : Store<Credential>, ICredentialsStore
{
    public CredentialsStore(ITagIdDbContext dbContext) : base(dbContext)
    {
    }

    IQueryable<Credential> ICredentialsStore.Query()
    {
        return Query;
    }
}


