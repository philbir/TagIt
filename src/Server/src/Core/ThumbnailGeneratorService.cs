using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagIt.Connectors;
using TagIt.Messaging;

namespace TagIt;
public class ThumbnailGeneratorService
{
    private readonly IThingService _thingService;
    private readonly IThingDataResolver _dataResolver;
    private readonly IConnectorFactory _connectorFactory;

    public ThumbnailGeneratorService(
        IThingService thingService,
        IThingDataResolver dataResolver,
        IConnectorFactory connectorFactory)
    {
        _thingService = thingService;
        _dataResolver = dataResolver;
        _connectorFactory = connectorFactory;
    }

    public async Task GenerateThumbmailsAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Thing thing = await _thingService.GetByIdAsync(id, cancellationToken);

        await GenerateThumbmailsAsync(thing, cancellationToken);
    }

    public async Task GenerateThumbmailsAsync(
        Thing thing,
        CancellationToken cancellationToken)
    {
        IConnector connector = await _connectorFactory.CreateAsync(
            thing.Source.ConnectorId,
            cancellationToken);

        if (connector.Description.HasThumbnailGenerator)
        {
            // TODO: Use connector thumbnail generator
        }
        else
        {
            ThingData data = await _dataResolver
                .GetOriginalAsync(thing, cancellationToken);



        }


    }
}
