namespace TagIt;

public interface IThingTypeService
{
    Task<ThingType> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<ThingType>> Query(CancellationToken cancellationToken);
}
