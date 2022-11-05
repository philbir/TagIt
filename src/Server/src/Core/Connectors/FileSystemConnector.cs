using Microsoft.AspNetCore.Http;

namespace TagIt.Connectors;

public class FileSystemConnector : Connector, IConnector
{
    public FileSystemConnector(IConnectorItemIdSerializer connectorItemIdSerializer)
        : base(connectorItemIdSerializer)
    { }

    public virtual Task<GetItemsResult> GetItemsAsync(
        GetItemsFilter filter,
        CancellationToken cancellationToken)
    {
        var path = Root;
        string searchPattern = "*";
        if (filter.Path is { })
        {
            path = Path.Combine(path, filter.Path);
        }

        if (filter.Filter is { })
        {
            searchPattern = $"*.{filter.Filter}";
        }

        SearchOption searchOption = filter.IncludeChildren
            ? SearchOption.AllDirectories
            : SearchOption.TopDirectoryOnly;

        var files = Directory.GetFiles(path, searchPattern, searchOption);

        IEnumerable<ConnectorItem> items = files.Select(x =>
        {
            var file = new FileInfo(x);
            return new ConnectorItem
            {
                Id = GetItemId(file.FullName),
                ConnectorId = Id,
                Location = file.Name,
                Name = Path.GetFileNameWithoutExtension(file.Name),
                Type = Path.GetExtension(file.Name).Split('.').Last().ToLower(),
                CreatedAt = file.CreationTime
            };
        });

        return Task.FromResult(new GetItemsResult { Items = items.ToList() });
    }

    private string SanitizeFilename(string fileName)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            fileName = fileName.Replace(c, '_');
        }

        return fileName;
    }

    public Task<Stream> DownloadAsync(string id, CancellationToken cancellationToken)
    {
        ItemIdentifier itemId = _itemIdSerializer.Deserialize(id);

        Stream stream = File.OpenRead(GetFullPath(itemId.id));

        return Task.FromResult(stream);
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        ItemIdentifier itemId = _itemIdSerializer.Deserialize(id);

        File.Delete(GetFullPath(itemId.id));

        return Task.CompletedTask;
    }

    public async Task<string> UploadAsync(string name, Stream stream, CancellationToken cancellationToken)
    {
        var path = GetFullPath(name.Split('/'));

        CreateDirectoryIfNotExists(Path.GetDirectoryName(path));

        using FileStream file = File.Create(path);

        stream.Seek(0, SeekOrigin.Begin);
        await stream.CopyToAsync(file, cancellationToken);

        return name;
    }

    public Task MoveAsync(string id, string path, CancellationToken cancellationToken)
    {
        ItemIdentifier itemId = _itemIdSerializer.Deserialize(id);

        var newFolder = Path.Combine(Root, path);
        CreateDirectoryIfNotExists(newFolder);

        File.Move(GetFullPath(itemId.id), Path.Combine(newFolder, itemId.id));

        return Task.CompletedTask;
    }

    private string GetFullPath(params string[] paths)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            if (i == paths.Length - 1)
            {
                paths[i] = SanitizeFilename(paths[i]);
            }
        }

        return Path.Combine(
            new string[] { Root }.Concat(paths).ToArray());
    }

    private void CreateDirectoryIfNotExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public Task<ProcessHookResult> ProcessWebHookRequestAsync(HttpContext httpContext, WebHook webHook)
    {
        throw new NotImplementedException();
    }

    public Task StartWatchingAsync(JobDefintion job, WatchOptions options, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ConnectorItemInfo> GetInfoAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

