using Microsoft.Extensions.DependencyInjection;
using TagIt.Store.Mongo;

namespace TagIt.Connectors;

public class DefaultConnectionManager : IConnectionManager
{
    private readonly IServiceProvider _serviceProvider;

    public DefaultConnectionManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public string[] ManagedTypes => new[] { "LFS", "GridFS" };

    public async Task<IConnector> CreateAsync(
        ConnectorDefintion defintion,
        CancellationToken cancellationToken)
    {
        IConnector? connector = null;

        IConnectorItemIdSerializer idSerializer = _serviceProvider
            .GetRequiredService<IConnectorItemIdSerializer>();

        switch (defintion.Type)
        {
            case "LFS":
                connector = new FileSystemConnector(idSerializer);
                break;
            case "GridFS":
                connector = new DatabaseFileConnector(
                    idSerializer,
                    _serviceProvider.GetRequiredService<IFileStore>());
                break;
            default:
                throw new NotSupportedException(
                    $"Not supported Type: {defintion.Type}");
        }

        await connector.InitializeAsync(defintion, cancellationToken)
            .ConfigureAwait(false);

        return connector;
    }
}
