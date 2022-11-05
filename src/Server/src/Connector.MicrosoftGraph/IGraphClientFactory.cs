using Microsoft.Graph;

namespace TagIt.MicrosoftGraph;

public interface IGraphClientFactory
{
    Task<GraphServiceClient> CreateClientAsync(
        Guid connectorId,
        CancellationToken cancellationToken);
}

