using TagIt.Store;

namespace TagIt;

public class ThingContentService : IThingContentService
{
    private readonly IThingContentStore _store;

    public ThingContentService(IThingContentStore store)
    {
        _store = store;
    }

    public async Task AddContentAsync(
        Guid thingId,
        IEnumerable<IThingContentData> contentData,
        CancellationToken cancellationToken)
    {
        var contents = new List<ThingContent>();

        foreach (IThingContentData data in contentData)
        {
            var content = new ThingContent()
            {
                Id = Guid.NewGuid(),
                ThingId = thingId,
                Source = data.Source,
                Data = data,
            };

            contents.Add(content);
        }

        await _store.InsertManyAsync(contents, cancellationToken);
    }

    public async Task<ThingContentAccessor> GetByThingIdAsync(
        Guid thingId,
        CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<ThingContent> contents = await _store.GetByThingIdAsync(thingId, cancellationToken);

            return new ThingContentAccessor(contents);
        }
        catch (Exception e)
        {
            return new ThingContentAccessor();
        }
    }
}

