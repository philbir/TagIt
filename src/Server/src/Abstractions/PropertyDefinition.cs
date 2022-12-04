using TagIt.Store;
#nullable disable

namespace TagIt;

public class PropertyDefinition : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public PropertyDataType DataType { get; set; }

    public IReadOnlyList<string> Options { get; set; }
}
