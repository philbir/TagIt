namespace TagIt;

public interface IThingContentService
{
    Task AddContentAsync(
        Guid thingId,
        IEnumerable<IThingContentData> contents,
        CancellationToken cancellationToken);

    Task<ThingContentAccessor?> GetByThingIdAsync(
        Guid thingId,
        CancellationToken cancellationToken);
}

public class ThingContentAccessor : IThingContentAccessor
{
    public IEnumerable<ThingContent> Items { get; } = Array.Empty<ThingContent>();

    public ThingContentAccessor()
    {
    }

    public ThingContentAccessor(IEnumerable<ThingContent> items)
    {
        Items = items;
    }

    public string GetAllText()
    {
        return string.Join('\n', Items.Select(x => x.Data.ToString()));
    }
}

public interface IThingContentAccessor
{
    string GetAllText();
}
