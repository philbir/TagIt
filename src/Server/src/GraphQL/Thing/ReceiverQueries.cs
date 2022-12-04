namespace TagIt.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class ReceiverQueries
{
    [UsePaging]
    [UseFiltering]
    public Task<IQueryable<Receiver>> GetReceiversAsync(
        [Service] IReceiverService service,
        CancellationToken cancellationToken)
    {
        return service.Query(cancellationToken);
    }
}
