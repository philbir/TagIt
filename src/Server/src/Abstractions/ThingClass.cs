#nullable disable
using TagIt.Store;

namespace TagIt;

public class ThingClass : EntityWithVersion, IEntityWithVersion
{
    public string Name { get; set; }

    public IReadOnlyList<ProperyDefintionLink> Properties { get; set; }
}

public class ProperyDefintionLink
{
    [ID(nameof(PropertyDefinition))]
    public Guid DefinitionId { get; set; }
}
