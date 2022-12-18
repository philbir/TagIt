namespace TagIt.Store;

public interface IThingStore
{
    Task<Thing> InsertAsync(Thing entity, CancellationToken cancellationToken);

    Task<Thing> UpdateAsync(Thing entity, CancellationToken cancellationToken);

    Task<Thing> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    public IQueryable <Thing> Query();
    Task UpdateThumbnailsAsync(Guid id, List<ThingThumbnail> thumbails, CancellationToken cancellationToken);
    Task UpdateStateAsync(Guid id, ThingState state, CancellationToken cancellationToken);
}
