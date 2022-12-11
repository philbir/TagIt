namespace TagIt;

public interface IThingContentService
{
    Task AddContentAsync(
        Guid thingId,
        IEnumerable<IThingContentData> contents,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<ThingContent>> GetByThingIdAsync(
        Guid thingId,
        CancellationToken cancellationToken);
}
