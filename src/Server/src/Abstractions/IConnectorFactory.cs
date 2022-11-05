namespace TagIt.Connectors;

public interface IConnectorFactory
{
    Task<IConnector> CreateAsync(Guid id, CancellationToken cancellationToken);
}
