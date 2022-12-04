namespace TagIt.Store;

public interface IThingClassStore
{
    public IQueryable<ThingClass> Query();

    Task<IReadOnlyList<ThingClass>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken);

    Task<ThingClass> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}
