namespace TagIt.Store;

public interface IThumbnailStore
{
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken);
    Task<byte[]> GetAsync(string id, CancellationToken cancellationToken);
    Task SaveAsync(string id, byte[] data, CancellationToken cancellationToken);
}
