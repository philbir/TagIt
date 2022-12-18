#nullable disable


using TagIt.Store;

namespace TagIt;

public class TagDefinition : IEntity, IDetectable
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Color { get; set; }

    public IReadOnlyList<DetectRule> DetectRules { get; } = Array.Empty<DetectRule>();

}
