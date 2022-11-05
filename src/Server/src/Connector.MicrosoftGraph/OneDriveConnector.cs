using Microsoft.Graph;
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
        GraphServiceClient client = await _graphClientFactory.CreateClientAsync(Id, cancellationToken);

        try
        {
            IDriveItemSearchCollectionPage items = await client.Me.Drive.Items[Root]
                .Search(filter.Filter)
                .Request()
                .Top(50)
                .GetAsync(cancellationToken);

            var result = new List<ConnectorItem>();

            foreach (DriveItem? item in items)
            {
                result.Add(
                    new ConnectorItem
                    {
                        Id = GetItemId(item.Id),
                        ConnectorId = Id,
                        Location = item.ParentReference?.Path,
                        Name = Path.GetFileNameWithoutExtension(item.Name),
                        Type = Path.GetExtension(item.Name).Split('.').Last().ToLower(),
                        CreatedAt = item.CreatedDateTime!.Value.DateTime
                    });
            }

            return new GetItemsResult
            {
                Items = result
            };
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Could no get items", ex);
        }

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
        //subscription.Resource = $"me/drive/items/{Root}";
        subscription.Resource = $"me/drive/root";
        subscription.ExpirationDateTime = DateTime.Now.AddDays(7);

        return subscription;
    }

    protected override string GetItemType(ChangeNotification change)
    {
        throw new NotSupportedException();
    }
}
