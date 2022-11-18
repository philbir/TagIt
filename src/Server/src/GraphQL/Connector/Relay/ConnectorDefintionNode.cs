using TagIt.Connectors;

namespace TagIt.GraphQL;

[Node]
[ExtendObjectType(typeof(ConnectorDefintion))]
public sealed class ConnectorDefintionNode
{
    [NodeResolver]
    public static Task<ConnectorDefintion?> GetThingAsync(
        [Service] IConnectorDefinitionService service,
        Guid id,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(id, cancellationToken);
    }
}
