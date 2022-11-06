using Microsoft.AspNetCore.Http;
using Serilog;
using TagIt.Messaging;
using static AnyDiff.DifferenceLines;

namespace TagIt.Connectors;

public class FileSystemConnector : Connector, IConnector
{
    private readonly IMessagePublisher _messagePublisher;

    public FileSystemConnector(
        IConnectorItemIdSerializer connectorItemIdSerializer,
        IMessagePublisher messagePublisher)
        : base(connectorItemIdSerializer)
    {
        _messagePublisher = messagePublisher;
    }

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
            return BuildConnectorItemFromFile(file);
        });

        return Task.FromResult(new GetItemsResult { Items = items.ToList() });
    }

    private ConnectorItem BuildConnectorItemFromFile(FileInfo file)
    {
        return new ConnectorItem
        {
            Id = GetItemId(file.FullName.Replace(Root, "").TrimStart(new[] { Path.DirectorySeparatorChar })),
            ConnectorId = Id,
            Location = file.Name,
            Name = Path.GetFileNameWithoutExtension(file.Name),
            Type = Path.GetExtension(file.Name).Split('.').Last().ToLower(),
            CreatedAt = file.CreationTime
        };
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

        Log.Information("LocalFileSystem: Download {Id}", itemId.id);

        Stream stream = File.OpenRead(GetFullPath(itemId.id));

        return Task.FromResult(stream);
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        ItemIdentifier itemId = _itemIdSerializer.Deserialize(id);
        Log.Information("LocalFileSystem: Delete {Id}", itemId.id);

        File.Delete(GetFullPath(itemId.id));

        return Task.CompletedTask;
    }

    public async Task<string> UploadAsync(string name, Stream stream, CancellationToken cancellationToken)
    {
        var path = GetFullPath(SanitizePaths(name.Split('/')));
        Log.Information("LocalFileSystem: Upload {Name}", name);

        CreateDirectoryIfNotExists(Path.GetDirectoryName(path));

        using FileStream file = File.Create(path);

        stream.Seek(0, SeekOrigin.Begin);
        await stream.CopyToAsync(file, cancellationToken);

        return name;
    }

    private string[] SanitizePaths(params string[] paths)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            if (i == paths.Length - 1)
            {
                paths[i] = SanitizeFilename(paths[i]);
            }
        }

        return paths;
    }

    public Task MoveAsync(string id, string path, CancellationToken cancellationToken)
    {
        ItemIdentifier itemId = _itemIdSerializer.Deserialize(id);


        var newFolder = Path.Combine(Root, path);
        CreateDirectoryIfNotExists(newFolder);

        string newPath = Path.Combine(newFolder, itemId.id);
        Log.Information("LocalFileSystem: Move {From} -> {To}", newPath);

        File.Move(GetFullPath(itemId.id), newPath);

        return Task.CompletedTask;
    }

    private string GetFullPath(params string[] paths)
    {
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

    public Task StartWatchingAsync(
        JobDefintion job,
        WatchOptions options,
        CancellationToken cancellationToken)
    {
        var watcher = new FileSystemWatcher(Root);

        watcher.Created += async (object sender, FileSystemEventArgs e) =>
        {
            Log.Information("FS Watcher: new file created {Path}", e.FullPath);

            await _messagePublisher.PublishAsync(
               new NewConnectorItemMessage(
                   BuildConnectorItemFromFile(new FileInfo(e.FullPath)),
                   job.Action),
               cancellationToken);
        };

        watcher.Filter = "*.pdf";
        watcher.IncludeSubdirectories = true;
        watcher.EnableRaisingEvents = true;

        Log.Information("Watcher started on {Path}", Root);

        return Task.CompletedTask;
    }

    public Task<ConnectorItemInfo> GetInfoAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


}

