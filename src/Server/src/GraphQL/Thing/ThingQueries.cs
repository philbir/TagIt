namespace TagIt.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class ThingQueries
{
    [UsePaging]
    [UseFiltering]
    public Task<IQueryable<Thing>> GetThingsAsync(
        [Service] IThingService service,
        CancellationToken cancellationToken)
    {
        return service.Query(cancellationToken);
    }

    public Task<Thing> GetThingByIdAsync(
        [ID(nameof(Thing))] Guid id,
        [Service] IThingService service,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(id, cancellationToken);
    }

    public Task<IQueryable<ThingType>> GetThingTypesAsync(
        [Service] IThingTypeService service,
        CancellationToken cancellationToken)
    {
        return service.Query(cancellationToken);
    }
}
