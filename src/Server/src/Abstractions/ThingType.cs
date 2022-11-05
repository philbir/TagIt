#nullable disable
using TagIt.Store;

namespace TagIt;

public class ThingType : EntityWithVersion, IEntityWithVersion
{
    public string Name { get; set; }

    public IReadOnlyList<Guid> ValidClasses { get; set; } = new List<Guid>();
    public IReadOnlyList<string> TypeMap { get; set; } = new List<string>();
}
