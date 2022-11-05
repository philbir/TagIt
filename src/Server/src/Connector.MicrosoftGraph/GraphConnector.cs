using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Graph;
using TagIt.Connectors;

namespace TagIt.MicrosoftGraph;

public class GraphConnector : Connector
{
    protected readonly IGraphClientFactory _graphClientFactory;
    private readonly IWebHookService _webHookService;

    public GraphConnector(
            IConnectorItemIdSerializer connectorItemIdSerializer,
            IGraphClientFactory graphClientFactory,
            IWebHookService webHookService)
            : base(connectorItemIdSerializer)
    {
        _graphClientFactory = graphClientFactory;
        _webHookService = webHookService;
    }

    protected virtual string GetItemType(ChangeNotification change)
    {
        return string.Empty;
    }

    protected virtual Subscription GetSubscription(WebHook hook, JobDefintion job)
    {
        var subscription = new Subscription
        {
            ChangeType = "created",
            NotificationUrl = hook.Url,
            ExpirationDateTime = DateTimeOffset.UtcNow.AddDays(1),
            ClientState = hook.ClientState,
            LatestSupportedTlsVersion = "v1_2"
        };

        return subscription;
    }

    public async Task<ProcessHookResult> ProcessWebHookRequestAsync(
        HttpContext httpContext,
        WebHook webHook)
    {
        StringValues validationToken = httpContext.Request.Query["validationToken"];

        

        if (validationToken is { Count: 1 })
        {
            httpContext.Response.StatusCode = 200;
            await httpContext.Response.WriteAsync(validationToken, httpContext.RequestAborted);

            return ProcessHookResult.Empty;
        }

        ChangeNotificationCollection notification = await ReadNotificationAsync(httpContext);

        ValidatesState(notification, webHook);

        return HandleChangeNotification(notification);
    }

    protected virtual ProcessHookResult HandleChangeNotification(
        ChangeNotificationCollection notification)
    {
        ProcessHookResult result = new ProcessHookResult();
        var items = new List<ConnectorItem>();

        foreach (ChangeNotification item in notification.Value)
        {
            if (item.ResourceData is { })
            {
                items.Add(new ConnectorItem
                {
                    ConnectorId = Id,
                    CreatedAt = DateTime.UtcNow,
                    Type = GetItemType(item),
                    Id = _itemIdSerializer.Serialize(
                        item.ResourceData.GetId(), Id),
                    Location = item.Resource
                });
            }
        }

        result.Items = items;
        result.RequestItemInfo = true;

        return result;
    }

    public async Task StartWatchingAsync(
        JobDefintion job,
        WatchOptions options,
        CancellationToken cancellationToken)
    {
        var hasExisting = await CheckExistingHookAsync(job, cancellationToken);
        if (hasExisting)
        {
            return;
        }

        WebHook hook = await _webHookService.CreateAsync(
            new CreateHookRequest("Microsoft.Graph", Id, job.Id), cancellationToken);

        GraphServiceClient client = await CreateClient(cancellationToken);
        Subscription subscription = GetSubscription(hook, job);

        Subscription response = await client.Subscriptions
            .Request()
            .AddAsync(subscription, cancellationToken);

        hook.Identifier = response.Id;
        hook.ExpiresAt = subscription.ExpirationDateTime;

        await _webHookService.UpdateAsync(hook, cancellationToken);
    }

    protected Task<GraphServiceClient> CreateClient(CancellationToken cancellationToken)
        => _graphClientFactory.CreateClientAsync(Id, cancellationToken);

    private async Task<bool> CheckExistingHookAsync(JobDefintion job, CancellationToken cancellationToken)
    {
        WebHook? existingHook = await _webHookService.GetByJobIdAsync(job.Id, cancellationToken);

        if (existingHook is { })
        {
            if (existingHook.Identifier is { } && existingHook.ExpiresAt.Value > DateTimeOffset.UtcNow)
            {
                //We have an active subscription
                return true;
            }
            else
            {
                await _webHookService.DeleteAsync(existingHook.Id, cancellationToken);
            }
        }

        return false;
    }

    private async Task<ChangeNotificationCollection> ReadNotificationAsync(HttpContext httpContext)
    {
        using var stream = new StreamReader(httpContext.Request.Body);
        var body = await stream.ReadToEndAsync();
        GraphServiceClient client = await _graphClientFactory.CreateClientAsync(Id, httpContext.RequestAborted);

        ChangeNotificationCollection notifications = client.HttpProvider.Serializer
            .DeserializeObject<ChangeNotificationCollection>(body);

        return notifications;
    }

    private void ValidatesState(ChangeNotificationCollection notification, WebHook webHook)
    {
        foreach (ChangeNotification change in notification.Value)
        {
            if (change.ClientState != webHook.ClientState)
            {
                //throw new ApplicationException($"Invalid state: {item.ClientState}");
            }
        }
    }
}
