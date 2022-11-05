namespace TagIt.Store;

public interface IAuditStore
{
    Task<EntityAuditEvent> InsertAsync(EntityAuditEvent entity, CancellationToken cancellationToken);
}
