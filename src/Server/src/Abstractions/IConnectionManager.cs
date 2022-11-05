namespace TagIt.Connectors;

public interface IConnectionManager
{
    string[] ManagedTypes { get; }

    Task<IConnector> CreateAsync(ConnectorDefintion defintion, CancellationToken cancellationToken);
}