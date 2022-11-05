using TagIt.Store;

namespace TagIt;

public class WebHook : IEntity
{
    public Guid Id { get; set; }

    public string Product { get; set; }

    public Guid ConnectorId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? ExpiresAt { get; set; }
    public Guid JobId { get; set; }
    public string Url { get; set; }
    public string Identifier { get; set; }

    public string ClientState { get; set; }
}
