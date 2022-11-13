using System.ComponentModel;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace TagIt.Store.Mongo;

public class FileStore : IFileStore
{
    private readonly ITagIdDbContext _dbContext;

    public FileStore(ITagIdDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> UploadAsync(
        string container,
        string id,
        Stream stream,
        CancellationToken cancellationToken)
    {
        IGridFSBucket gridFs = _dbContext.CreateGridFsBucket(container);

        ObjectId objectId = await gridFs.UploadFromStreamAsync(
            id,
            stream,
            new GridFSUploadOptions(),
            cancellationToken);

        return objectId.ToString();

    }

    public async Task<Stream> DownloadAsync(
        string container,
        string id,
        CancellationToken cancellationToken)
    {
        IGridFSBucket gridFs = _dbContext.CreateGridFsBucket(container);

        var stream = new MemoryStream();
        await gridFs.DownloadToStreamByNameAsync(
            id,
            stream, new GridFSDownloadByNameOptions(),
            cancellationToken);

        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }

    public async Task DeleteAsync(string container, string id, CancellationToken cancellationToken)
    {
        IGridFSBucket gridFs = _dbContext.CreateGridFsBucket(container);

        IAsyncCursor<GridFSFileInfo> cursor = await gridFs.FindAsync(
             Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, id),
             new GridFSFindOptions(),
             cancellationToken);

        GridFSFileInfo file = await cursor.FirstOrDefaultAsync(
            cancellationToken);

        if (file != null)
        {
            await gridFs.DeleteAsync(file.Id, cancellationToken);
        }
    }
}
