#nullable disable
using TagIt.Store;

namespace TagIt;

public class ThingClass : EntityWithVersion, IEntityWithVersion, IDetectable
{
    public string Name { get; set; }

    public IReadOnlyList<PropertyDefinitionLink> Properties { get; set; }

    public IReadOnlyList<DetectRule> DetectRules { get; set; } = Array.Empty<DetectRule>();

}

public class PropertyDefinitionLink
{
    public Guid DefinitionId { get; set; }
}
