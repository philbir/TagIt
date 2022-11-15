namespace TagIt.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
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

    public Task<Correspondent> GetCorrespondentByIdAsync(
        [ID(nameof(Correspondent))] Guid id,
        [Service] ICorrespondentService service,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(id, cancellationToken);
    }
}

