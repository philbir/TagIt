namespace TagIt.Connectors;

public interface IConnectionManager
{
    string[] ManagedTypes { get; }

    ValueTask<IConnector> CreateAsync(
        ConnectorDefintion defintion,
        CancellationToken cancellationToken);
}
