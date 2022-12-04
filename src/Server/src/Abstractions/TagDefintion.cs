#nullable disable


using TagIt.Store;

namespace TagIt;

public class TagDefinition : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Color { get; set; }
}
