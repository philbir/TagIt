namespace TagIt.Connectors;

public interface IConnectorFactory
{
    ValueTask<IConnector> CreateAsync(Guid id, CancellationToken cancellationToken);
    ValueTask<IConnector> CreateDefaultAsync(CancellationToken cancellationToken);
}
