#nullable disable
namespace TagIt;

public class ThingRelation
{
    public Guid Id { get; set; }

    public RelationType Type { get; set; }

    public Guid From { get; set; }

    public Guid To { get; set; }
}
