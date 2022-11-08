using Microsoft.Extensions.DependencyInjection;
using TagIt.Connectors;

namespace TagIt.MicrosoftGraph;

public class MicrosoftGraphConnectionManager : IConnectionManager
{
    private readonly IServiceProvider _serviceProvider;

    public MicrosoftGraphConnectionManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public string[] ManagedTypes => new[] { "OneDrive", "OutlookMail" };

    public async ValueTask<IConnector> CreateAsync(
        ConnectorDefintion defintion,
        CancellationToken cancellationToken)
    {
        IConnector? connector = null;

        IConnectorItemIdSerializer idSerializer = _serviceProvider
            .GetRequiredService<IConnectorItemIdSerializer>();

        IGraphClientFactory graphClientFactory = _serviceProvider
            .GetRequiredService<IGraphClientFactory>();

        switch (defintion.Type)
        {
            case "OneDrive":
                connector = new OneDriveConnector(
                    idSerializer,
                    graphClientFactory,
                    _serviceProvider.GetRequiredService<IWebHookService>());
                break;
            case "OutlookMail":
                connector = new MailConnector(
                    idSerializer,
                    graphClientFactory,
                    _serviceProvider.GetRequiredService<IWebHookService>());
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
