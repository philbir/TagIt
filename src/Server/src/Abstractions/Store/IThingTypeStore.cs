namespace TagIt.Store;

public interface IThingTypeStore
{
    public IQueryable<ThingType> Query();

    Task<ThingType?> GetByTypeMapAsync(
        string type,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<ThingType>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken);

    Task<ThingType> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}
