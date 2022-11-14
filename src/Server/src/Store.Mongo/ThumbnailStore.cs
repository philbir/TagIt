using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace TagIt.Store.Mongo;

public class ThumbnailStore : IThumbnailStore
{
    private readonly ITagIdDbContext _dbContext;
    private readonly IGridFSBucket _gridFs;

    public ThumbnailStore(ITagIdDbContext dbContext)
    {
        _dbContext = dbContext;
        _gridFs = _dbContext.CreateGridFsBucket("thumbnails");
    }

    public Task<byte[]> GetAsync(string id, CancellationToken cancellationToken)
    {
        return _gridFs.DownloadAsBytesByNameAsync(
            id,
            new GridFSDownloadByNameOptions(),
            cancellationToken);
    }

    public async Task SaveAsync(
        string id,
        byte[] data,
        CancellationToken cancellationToken)
    {
        await _gridFs.UploadFromBytesAsync(
            id,
            data,
            new GridFSUploadOptions(),
            cancellationToken);
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        FilterDefinition<GridFSFileInfo> filter = Builders<GridFSFileInfo>.Filter
            .Eq(x => x.Filename, id);

        IAsyncCursor<GridFSFileInfo> cursor = await _gridFs
            .FindAsync(filter, options: null, cancellationToken);

        GridFSFileInfo? file = cursor.FirstOrDefault(cancellationToken);

        if (file != null)
        {
            await _gridFs.DeleteAsync(file.Id, cancellationToken);
            return true;
        }

        return false;
    }

}
