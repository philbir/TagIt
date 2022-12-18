namespace TagIt.GraphQL;

[QueryType]
public class CorrespondentQueries
{
    [UsePaging]
    [UseFiltering]
    public Task<IQueryable<Correspondent>> GetCorrespondentsAsync(
        [Service] ICorrespondentService service,
        CancellationToken cancellationToken)
    {
        return service.Query(cancellationToken);
    }

    [NodeResolver]
    public Task<Correspondent> GetCorrespondentByIdAsync(
        Guid id,
        [Service] ICorrespondentService service,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(id, cancellationToken);
    }
}
