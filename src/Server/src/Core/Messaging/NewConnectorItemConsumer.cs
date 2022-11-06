using MassTransit;
using Serilog;
using TagIt.Connectors;

namespace TagIt.Messaging;

public class NewConnectorItemConsumer : IConsumer<NewConnectorItemMessage>
{
    private readonly IThingService _thingService;
    private readonly IConnectorFactory _connectorFactory;

    public NewConnectorItemConsumer(
        IThingService thingService,
        IConnectorFactory connectorFactory)
    {
        _thingService = thingService;
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
            Type = item.Type,
            Data = new List<ThingData>
            {
                new()
                {
                    Id = item.Id,
                    ConnectorId = item.ConnectorId,
                    Location = item.Location,
                    Type = item.Type,
                }
            },
            Action = context.Message.Action
        };

        await _thingService.AddThingAsync(addRequest, context.CancellationToken);
    }
}
