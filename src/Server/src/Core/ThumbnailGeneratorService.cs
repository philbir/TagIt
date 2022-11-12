using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagIt.Connectors;
using TagIt.Messaging;
using TagIt.Store;

namespace TagIt;
public class ThumbnailGeneratorService
{
    public async Task GenerateThumbmailsAsync(
        Thing thing,
        CancellationToken cancellationToken)
    {
        







    }


}

public class PdfImageExtractor
{
    public string[] SupportedTypes { get; } = new[] { "application/pdf" };

    public Task<IEnumerable<Thumbnail>> ExtractAsync(
        Thing thing,
        CancellationToken cancellationToken)
    {
        return null;

    }

}

public class ThingDataResolver
{
    private readonly IThingStore _thingStore;
    private readonly IConnectorFactory _connectorFactory;

    public ThingDataResolver(
        IThingStore thingStore,
        IConnectorFactory connectorFactory)
    {
        _thingStore = thingStore;
        _connectorFactory = connectorFactory;
    }


    public async Task<ThingData> GetOriginalAsync(
        Thing thing,
        CancellationToken cancellationToken)
    {
        //TODO: Get original
        ThingDataReference dataRef = thing.Data.Single(x => x.Type == DataRefNames.Original);

        IConnector connector = await _connectorFactory
            .CreateAsync(dataRef.ConnectorId, cancellationToken);

        Stream data = await connector.DownloadAsync(dataRef.Id, cancellationToken);

        return new ThingData
        {
            ConnectorId = dataRef.ConnectorId,
            Id = dataRef.Id,
            ContentType = dataRef.ContentType,
        };
    }
}
