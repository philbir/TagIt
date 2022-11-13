using Microsoft.Graph;
using Serilog;
using TagIt.Connectors;

namespace TagIt.MicrosoftGraph;

public class OneDriveConnector : GraphConnector, IConnector
{
    public OneDriveConnector(IConnectorItemIdSerializer connectorItemIdSerializer,
        IGraphClientFactory graphClientFactory, IWebHookService webHookService)
        : base(connectorItemIdSerializer, graphClientFactory, webHookService)
    {
    }

    public async Task DeleteAsync(
        string id,
        CancellationToken cancellationToken)
    {
        ItemIdentifier itemId = _itemIdSerializer.Deserialize(id);
        Log.Information("OneDrive: Delete {Id}", itemId.id);

        GraphServiceClient client = await CreateClient(cancellationToken);

        await client.Me.Drive.Items[itemId.id]
            .Request()
            .DeleteAsync(cancellationToken);
    }

    public async Task<Stream> DownloadAsync(
        string id,
        CancellationToken cancellationToken)
    {
        ItemIdentifier itemId = _itemIdSerializer.Deserialize(id);
        Log.Information("OneDrive: Download {Id}", itemId.id);

        GraphServiceClient client = await CreateClient(cancellationToken);

        Stream stream = await client.Me.Drive.Items[itemId.id].Content
            .Request()
            .GetAsync(cancellationToken);

        return stream;
    }

    public Task<ConnectorItemInfo> GetInfoAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<GetItemsResult> GetItemsAsync(
        GetItemsFilter filter,
        CancellationToken cancellationToken)
    {
        Log.Information("OneDrive GetItems in {Root} with filter {Filter}", Root, filter.Filter);

        GraphServiceClient client = await _graphClientFactory.CreateClientAsync(Id, cancellationToken);

        try
        {
            // Search is better for recursive search, but needs more time to be availlable when a new item is added to OneDrive

            //IDriveItemSearchCollectionPage items = await client.Me.Drive.Items[Root]
            //    .Search(filter.Filter)
            //    .Request()
            //    .Top(50)
            //    .GetAsync(cancellationToken);

            IDriveItemChildrenCollectionPage items = await client.Me.Drive.Items[Root].Children
                .Request()
                .Filter(filter.Filter)
                .Top(50)
                .GetAsync(cancellationToken);

            var result = new List<ConnectorItem>();

            foreach (DriveItem? item in items)
            {
                result.Add(
                    new ConnectorItem
                    {
                        Id = item.Id,
                        UniqueId = GetItemId(item.Id),
                        ContentType = GetContentType(item.Name),
                        ConnectorId = Id,
                        Location = item.ParentReference?.Path,
                        Name = Path.GetFileNameWithoutExtension(item.Name),
                        CreatedAt = item.CreatedDateTime!.Value.DateTime
                    });
            }

            Log.Information("{Count} Items found in OneDrive", items.Count);

            return new GetItemsResult
            {
                Items = result
            };
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error loading items");
            throw new ApplicationException("Could no get items", ex);
        }

    }

    private string? GetContentType(string filename)
    {
        var extension = Path.GetExtension(filename).ToLower();
        if (extension is { })
        {
            return extension.Replace(".", "");
        }

        return null;
    }

    public Task MoveAsync(
        string id,
        string path,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UploadAsync(
        string name,
        Stream stream,
        CancellationToken cancellationToken)
    {
        GraphServiceClient client = await CreateClient(cancellationToken);

        var uploadProps = new DriveItemUploadableProperties
        {
            ODataType = null,
            AdditionalData = new Dictionary<string, object>
            {
                { "@microsoft.graph.conflictBehavior", "replace" }
            }
        };

        UploadSession uploadSession = await client.Me.Drive.Items[Root]
            .ItemWithPath(name)
            .CreateUploadSession(uploadProps)
            .Request()
            .PostAsync(cancellationToken);

        int maxSliceSize = 320 * 1024;
        var fileUploadTask = new LargeFileUploadTask<DriveItem>(uploadSession, stream, maxSliceSize);

        IProgress<long> progress = new Progress<long>(prog =>
        {
            Console.Write($"Uploading... ({prog})");
        });

        UploadResult<DriveItem> uploadResult = await fileUploadTask.UploadAsync(progress);

        return uploadResult.ItemResponse.Id;
    }

    protected override Subscription GetSubscription(WebHook hook, JobDefintion job)
    {
        Subscription subscription = base.GetSubscription(hook, job);
        subscription.ChangeType = "updated";
        subscription.Resource = $"me/drive/root";
        subscription.ExpirationDateTime = DateTime.Now.AddDays(7);

        return subscription;
    }

    protected override ProcessHookResult HandleChangeNotification(ChangeNotificationCollection notification)
    {
        // Needs special handling here
        // OneDrive does not include an data in the notification, the changes need to be handled using the delta api
        // https://learn.microsoft.com/en-us/graph/api/driveitem-delta?view=graph-rest-1.0&tabs=http

        return ProcessHookResult.Empty;
    }

    protected override string GetItemContentType(ChangeNotification change)
    {
        throw new NotSupportedException();
    }
}
