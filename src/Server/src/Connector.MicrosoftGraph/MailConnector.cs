using System.Text.Json;
using Microsoft.Graph;
using TagIt.Connectors;

namespace TagIt.MicrosoftGraph;

public class MailConnector : GraphConnector, IConnector
{
    private readonly IWebHookService _webHookService;

    public MailConnector(
        IConnectorItemIdSerializer connectorItemIdSerializer,
        IGraphClientFactory graphClientFactory,
        IWebHookService webHookService)
        : base(connectorItemIdSerializer, graphClientFactory, webHookService)
    {
        _webHookService = webHookService;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        ItemIdentifier itemId = _itemIdSerializer.Deserialize(id);

        GraphServiceClient client = await CreateClient(cancellationToken);

        await client.Me.Messages[itemId.id]
            .Request()
            .DeleteAsync(cancellationToken);
    }

    protected override string GetItemContentType(ChangeNotification change)
    {
        return "eml";
    }

    protected override Subscription GetSubscription(WebHook hook, JobDefintion job)
    {
        Subscription subscription = base.GetSubscription(hook, job);
        subscription.Resource = "me/mailFolders('Inbox')/messages";

        return subscription;
    }

    public async Task<ConnectorItemInfo> GetInfoAsync(string id, CancellationToken cancellationToken)
    {
        ItemIdentifier itemId = _itemIdSerializer.Deserialize(id);

        GraphServiceClient client = await CreateClient(cancellationToken);

        Message email = await client.Me.Messages[itemId.id]
            .Request()
            .GetAsync();

        var info = new ConnectorItemInfo()
        {
            Title = email.Subject
        };

        EmailMessage data = MapToEmailMessage(email);
        var json = JsonSerializer.Serialize(data);
        info.Data.Add("Email_Data", json);

        return info;
    }

    private EmailMessage MapToEmailMessage(Message email)
    {
        var emailData = new EmailMessage
        {
            Subject = email.Subject,
            Body = new EmailBody
            {
                Body = email.Body.Content,
                ContextType = email.Body.ContentType.ToString(),
                Preview = email.BodyPreview
            },
            ReceivedAt = email.ReceivedDateTime.Value,
            From = new EmailAddress
            {
                Address = email.From.EmailAddress.Address,
                Name = email.From.EmailAddress.Name
            },
            To = email.ToRecipients.Select(m =>
            {
                return new EmailAddress
                {
                    Address = m.EmailAddress.Address,
                    Name = m.EmailAddress.Name
                };
            }).ToList(),
            Cc = email.CcRecipients.Select(m =>
            {
                return new EmailAddress
                {
                    Address = m.EmailAddress.Address,
                    Name = m.EmailAddress.Name
                };
            }).ToList(),
        };

        return emailData;
    }

    public async Task<Stream> DownloadAsync(string id, CancellationToken cancellationToken)
    {
        ItemIdentifier itemId = _itemIdSerializer.Deserialize(id);

        GraphServiceClient client = await CreateClient(cancellationToken);

        Stream email = await client.Me.Messages[itemId.id].Content
            .Request()
            .GetAsync();

        return email;
    }

    public Task<GetItemsResult> GetItemsAsync(GetItemsFilter filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task MoveAsync(string id, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> UploadAsync(string name, Stream stream, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
