namespace TagIt;

public interface IThingClassService
{
    Task<ThingClass> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ThingClass>> GetManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<IQueryable<ThingClass>> Query(CancellationToken cancellationToken);
}