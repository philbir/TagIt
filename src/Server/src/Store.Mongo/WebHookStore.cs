

namespace TagIt.Store.Mongo;

public class WebHookStore : Store<WebHook>, IWebHookStore
{
    public WebHookStore(ITagIdDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<WebHook?> GetByJobIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Query.Where(x => x.JobId == id)
            .SingleOrDefaultAsync(cancellationToken);
    }



    IQueryable<WebHook> IWebHookStore.Query()
    {
        return Query;
    }
}
