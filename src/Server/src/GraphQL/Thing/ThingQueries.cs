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

    public Task<IQueryable<ThingType>> GetThingTypesAsync(
        [Service] IThingTypeService service,
        CancellationToken cancellationToken)
    {
        return service.Query(cancellationToken);
    }
}

