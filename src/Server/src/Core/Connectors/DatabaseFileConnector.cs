using Microsoft.AspNetCore.Http;
using TagIt.Store.Mongo;

namespace TagIt.Connectors;

public class DatabaseFileConnector : Connector, IConnector
{
    private readonly IFileStore _fileStore;

    public DatabaseFileConnector(
        IConnectorItemIdSerializer idSerializer,
        IFileStore fileStore)
         : base(idSerializer)
    {
        _fileStore = fileStore;
        Root = "default";
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        return _fileStore.DeleteAsync(Root, id, cancellationToken);
    }

    public Task<Stream> DownloadAsync(string id, CancellationToken cancellationToken)
    {
        return _fileStore.DownloadAsync(Root, id, cancellationToken);
    }

    public Task<ConnectorItemInfo> GetInfoAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<GetItemsResult> GetItemsAsync(GetItemsFilter filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task MoveAsync(string id, string path, CancellationToken cancellationToken)
    {
        //Nothing to move
        return Task.CompletedTask;
    }

    public Task<ProcessHookResult> ProcessWebHookRequestAsync(HttpContext httpContext, WebHook webHook)
    {
        throw new NotImplementedException();
    }

    public Task StartWatchingAsync(JobDefintion job, WatchOptions options, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> UploadAsync(string path, Stream stream,  CancellationToken cancellationToken)
    {
        return _fileStore.UploadAsync(Root, path, stream, cancellationToken);
    }
}
