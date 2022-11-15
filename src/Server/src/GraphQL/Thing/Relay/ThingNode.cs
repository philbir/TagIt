namespace TagIt.GraphQL;

[Node]
[ExtendObjectType(typeof(Thing))]
public sealed class ThingNode
{
    [NodeResolver]
    public static Task<Thing?> GetThingAsync(
        [Service] IThingService service,
        Guid id,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(id, cancellationToken);
    }
}

