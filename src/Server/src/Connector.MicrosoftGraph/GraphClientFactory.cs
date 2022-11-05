using Microsoft.Graph;
using TagIt.Store;

namespace TagIt.MicrosoftGraph;

public class GraphClientFactory : IGraphClientFactory
{
    private readonly ICredentialStoreTokenManager _tokenManager;
    private readonly IConnectorStore _connectorStore;

    public GraphClientFactory(
        ICredentialStoreTokenManager tokenManager,
        IConnectorStore connectorStore)
    {
        _tokenManager = tokenManager;
        _connectorStore = connectorStore;
    }

    public async Task<GraphServiceClient> CreateClientAsync(
        Guid connectorId,
        CancellationToken cancellationToken)
    {
        ConnectorDefintion connector = await _connectorStore.GetByIdAsync(connectorId, cancellationToken);
        string token = await _tokenManager.GetAccessToken(connector.CredentialId.Value, cancellationToken);

        var authProvider = new DelegateAuthenticationProvider(request =>
        {
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            return Task.CompletedTask;
        });

        var client = new GraphServiceClient(authProvider);

        return client;
    }
}
