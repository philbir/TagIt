using TagIt.Connectors;

namespace TagIt.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class ConnectorQueries
{
    [UsePaging]
    [UseFiltering]
    public Task<IQueryable<ConnectorDefintion>> GetConnectorsAsync(
        [Service] IConnectorDefinitionService service,
        CancellationToken cancellationToken)
    {
        return service.Query(cancellationToken);
    }
}
