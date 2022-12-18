namespace TagIt.Store;

public interface IThingContentStore
{
    Task<ThingContent> InsertAsync(ThingContent content, CancellationToken cancellationToken);

    public Task InsertManyAsync(IEnumerable<ThingContent> contents, CancellationToken cancellationToken);

    Task<ThingContent> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public IQueryable <ThingContent> Query();

    Task<IReadOnlyList<ThingContent>> GetByThingIdAsync(
        Guid thingId,
        CancellationToken cancellationToken);
}
