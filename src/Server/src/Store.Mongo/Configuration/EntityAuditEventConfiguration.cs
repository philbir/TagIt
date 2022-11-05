using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class EntityAuditEventConfiguration :
    IMongoCollectionConfiguration<EntityAuditEvent>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<EntityAuditEvent> builder)
    {
        builder
            .WithDefaults(CollectionNames.EntityAudit);
    }
}
