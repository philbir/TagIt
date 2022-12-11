#nullable disable
using TagIt.Store;

namespace TagIt;

public class ThingContent : IEntity
{
    public Guid Id { get; set; }

    public Guid ThingId { get; set; }

    public string Source { get; set; }

    public IThingContentData Data { get; set; }
}
