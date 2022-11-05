namespace TagIt;

public class TagType
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Color { get; set; }

    public IEnumerable<Guid> ThingTypes { get; set; }

    public IEnumerable<string> Values { get; set; }
}
