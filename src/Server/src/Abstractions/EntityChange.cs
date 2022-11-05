#nullable disable


namespace TagIt;

public class EntityChange
{
    public string Property { get; set; }

    public string Path { get; set; }

    public string? Before { get; set; }

    public string? After { get; set; }

    public string? Delta { get; set; }

    public int? ArrayIndex { get; set; }
}
