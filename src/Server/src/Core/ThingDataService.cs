using TagIt.Connectors;
using TagIt.Store;

namespace TagIt;

public class ThingDataResolver : IThingDataResolver
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
            Stream = data
        };
    }
}
