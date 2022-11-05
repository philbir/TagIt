namespace TagIt.Store.Mongo;

public interface IFileStore
{
    Task DeleteAsync(string container, string id, CancellationToken cancellationToken);
    Task<Stream> DownloadAsync(string container, string id, CancellationToken cancellationToken);
    Task<string> UploadAsync(string container, string id, Stream stream, CancellationToken cancellationToken);
}
