using Microsoft.AspNetCore.Http;
using TagIt.Store;
#nullable disable

namespace TagIt;

public class ConnectorDefintion : IEntity
{
    public Guid Id { get; set; }

    public string Type { get; set; }

    public string Name { get; set; }

    public string Root { get; set; }

    public IDictionary<string, string> Properties { get; set; }

    public Guid? CredentialId { get; set; }
}

public interface IConnector
{
    public Guid Id { get; set; }
    public ConnectorDescription Description { get; }
    public Task DeleteAsync(string id, CancellationToken cancellationToken);
    public Task<Stream> DownloadAsync(string id, CancellationToken cancellationToken);
    public Task<ConnectorItemInfo> GetInfoAsync(string id, CancellationToken cancellationToken);
    public Task<GetItemsResult> GetItemsAsync(GetItemsFilter filter, CancellationToken cancellationToken);
    public ValueTask InitializeAsync(ConnectorDefintion defintion, CancellationToken cancellationToken);
    public Task MoveAsync(string id, string path, CancellationToken cancellationToken);
    public Task<ProcessHookResult> ProcessWebHookRequestAsync(HttpContext httpContext, WebHook webHook);
    public Task StartWatchingAsync(
        JobDefintion job,
        WatchOptions options,
        CancellationToken cancellationToken);

    public Task<string> UploadAsync(string name, Stream stream, CancellationToken cancellationToken);
}

public class ConnectorDescription
{
    public string? Description { get; set; }

    public bool HasThumbnailGenerator { get; set; }
}


public class ConnectorItem
{
    public string Id { get; set; }

    public Guid ConnectorId { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    public DateTime CreatedAt { get; set; }
    public string UniqueId { get; set; }
    public string ContentType { get; set; }
}

public class ConnectorItemInfo
{
    public string Title { get; set; }

    public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
}


public class ProcessHookResult
{
    public IReadOnlyList<ConnectorItem> Items { get; set; } = new List<ConnectorItem>();
    public bool RequestItemInfo { get; set; }

    public static ProcessHookResult Empty => new ProcessHookResult();
}

public class GetItemsResult
{
    public IReadOnlyList<ConnectorItem> Items { get; set; }

    public bool HasMore { get; set; }
}

public class GetItemsFilter
{
    public string Path { get; set; }

    public string? Filter { get; set; }

    public bool IncludeChildren { get; set; }

    public int Count { get; set; }
}


public class WatchOptions
{

}

public record ItemFilter(ItemFilteType Type, string Value);

public enum ItemFilteType
{
    Path,
    FileType,
    IncludeChildrens
}
