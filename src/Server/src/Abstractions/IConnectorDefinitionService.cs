namespace TagIt.Connectors;

public interface IConnectorDefinitionService
{
    Task<ConnectorDefintion> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<ConnectorDefintion>> Query(CancellationToken cancellationToken);
}