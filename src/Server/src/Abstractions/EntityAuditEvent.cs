#nullable disable

using TagIt.Store;

namespace TagIt;

public class EntityAuditEvent : IEntity
{
    public Guid Id { get; set; }

    public Guid EntityId { get; set; }

    public string EntityName { get; set; }

    public string Action { get; set; }

    public DateTime Timestamp { get; set; }

    public IEnumerable<EntityChange> Changes { get; set; }

    public int Version { get; set; }

    public Guid? UserId { get; set; }
}
