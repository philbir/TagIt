namespace TagIt.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class TagDefinitionQueries
{
    [UsePaging]
    [UseFiltering]
    public Task<IQueryable<TagDefinition>> GetTagDefintionsAsync(
        [Service] ITagDefinitionService service,
        CancellationToken cancellationToken)
    {
        return service.Query(cancellationToken);
    }
}
