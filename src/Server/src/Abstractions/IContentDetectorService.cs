namespace TagIt;

public interface IContentDetectorService
{
    IReadOnlyList<DetectResult<T>> Detect<T>(
        IEnumerable<T> items,
        IThingContentAccessor contentAccessor) where T : IDetectable;

    IReadOnlyList<DetectResult<T>> Detect<T>(IEnumerable<T> items, string value) where T : IDetectable;
}

public interface IDetectable
{
    string Name { get; }
    IReadOnlyList<DetectRule> DetectRules { get; }
}

public class DetectResult<T> where T: IDetectable
{
    public DetectResult(T item, int scrore)
    {
        Item = item;
        Scrore = scrore;
    }

    public T Item { get; }

    public int Scrore { get; } = default!;
}

public class DetectRule
{
    public DetectRuleMode Mode { get; set; }
    public string Expression { get; set; }

    public int Weight { get; set; }

    public string Field { get; set; }
}

public enum DetectRuleMode
{
    Regex
}
