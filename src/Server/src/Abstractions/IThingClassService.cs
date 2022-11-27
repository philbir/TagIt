namespace TagIt;

public interface IThingClassService
{
    Task<ThingClass> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<ThingClass>> Query(CancellationToken cancellationToken);
}