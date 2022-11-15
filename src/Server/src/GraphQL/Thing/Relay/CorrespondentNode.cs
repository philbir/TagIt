namespace TagIt.GraphQL;

[Node]
[ExtendObjectType(typeof(Correspondent))]
public sealed class CorrespondentNode
{
    [NodeResolver]
    public static Task<Correspondent?> GetCorrespondentAsync(
        [Service] ICorrespondentService service,
        Guid id,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(id, cancellationToken);
    }
}

