#nullable disable

using TagIt.Store;

namespace TagIt;

public class Correspondent : IEntity, IDetectable
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IReadOnlyList<DetectRule> DetectRules { get; set; } = new List<DetectRule>();
}
