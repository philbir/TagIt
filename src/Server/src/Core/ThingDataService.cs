using TagIt.Connectors;
using TagIt.Store;

namespace TagIt;

public class ThingDataService : IThingDataService
{
    private readonly IThingStore _thingStore;
    private readonly IConnectorFactory _connectorFactory;

    public ThingDataService(
        IThingStore thingStore,
        IConnectorFactory connectorFactory)
    {
        _thingStore = thingStore;
        _connectorFactory = connectorFactory;
    }

    public async Task AddDataAsync(Thing thing, AddThingDataRequest request, CancellationToken cancellationToken)
    {
        IConnector connector = await _connectorFactory.CreateDefaultAsync(cancellationToken);

        var dataRef = new ThingDataReference
        {
            ConnectorId = connector.Id,
            ContentType = Path.GetExtension(request.Filename).Split('.').Last().ToLower(),
            Type = request.Type
        };

        dataRef.Id = await connector.UploadAsync(request.Filename, request.Stream, cancellationToken);

        var data = thing.Data.ToList();
        data.Add(dataRef);

        thing.Data = data;

        await _thingStore.UpdateAsync(thing, cancellationToken);
    }

    public async Task<ThingData> GetByTypeAsync(
        Thing thing,
        string type,
        CancellationToken cancellationToken)
    {
        ThingDataReference dataRef = thing.Data.Single(x => x.Type == type);

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

    public Task<ThingData> GetOriginalAsync(
        Thing thing,
        CancellationToken cancellationToken)
            => GetByTypeAsync(thing, DataRefNames.Original, cancellationToken);

    public Task<ThingData> GetPdfArchiveAsync(
        Thing thing,
        CancellationToken cancellationToken)
        => GetByTypeAsync(thing, DataRefNames.PdfArchive, cancellationToken);
}
