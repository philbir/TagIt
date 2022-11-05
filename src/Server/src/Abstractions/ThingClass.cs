#nullable disable
using TagIt.Store;

namespace TagIt;

public class ThingClass : EntityWithVersion, IEntityWithVersion
{
    public string Name { get; set; }
}
