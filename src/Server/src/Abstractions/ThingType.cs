#nullable disable
using TagIt.Store;

namespace TagIt;

public class ThingType : EntityWithVersion, IEntityWithVersion, IDetectable
{
    public string Name { get; set; }

    public IReadOnlyList<Guid> ValidClasses { get; set; } = new List<Guid>();

    public IReadOnlyList<string> ContentTypeMap { get; set; } = new List<string>();

    public IReadOnlyList<DetectRule> DetectRules { get; set; }
}
