using MassTransit;
using Serilog;
using TagIt.Connectors;
namespace TagIt.Messaging;

public class NewConnectorItemConsumer : IConsumer<NewConnectorItemMessage>
{
    private readonly IThingIngestService _ingestService;
    private readonly IConnectorFactory _connectorFactory;

    public NewConnectorItemConsumer(
        IThingIngestService ingestService,
        IConnectorFactory connectorFactory)
    {
        _ingestService = ingestService;
        _connectorFactory = connectorFactory;
    }

    public async Task Consume(ConsumeContext<NewConnectorItemMessage> context)
    {
        // TODO: Handle item locking
        ConnectorItem item = context.Message.Item;

        Log.Information("NewConnectorItemMessage: {Id}", item.Id);

        if (context.Message.RequestItemInfo)
        {
            IConnector connector = await _connectorFactory.CreateAsync(
                item.ConnectorId,
                context.CancellationToken);

            ConnectorItemInfo info = await connector
                .GetInfoAsync(item.Id, context.CancellationToken);

            item.Name = info.Title;
        }

        var addRequest = new AddThingRequest
        {
            Title = item.Name,
            ContentType = item.ContentType,
            Source = new ThingSource
            {
                ConnectorId = item.ConnectorId,
                Id = item.Id,
                UniqueId = item.UniqueId
            },
            Action = context.Message.Action
        };

        await _ingestService.AddAsync(addRequest, context.CancellationToken);
    }
}
