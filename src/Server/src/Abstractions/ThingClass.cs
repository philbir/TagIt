#nullable disable
using TagIt.Store;

namespace TagIt;

public class ThingClass : EntityWithVersion, IEntityWithVersion
{
    public string Name { get; set; }

    public IReadOnlyList<PropertyDefinitionLink> Properties { get; set; }
}

public class PropertyDefinitionLink
{
    [ID(nameof(PropertyDefinition))]
    public Guid DefinitionId { get; set; }
}
