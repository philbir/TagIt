namespace TagIt.Store.Mongo;

public class AuditStore : Store<EntityAuditEvent>, IAuditStore
{
    public AuditStore(ITagIdDbContext dbContext)
        : base(dbContext)
    {
    }
}
