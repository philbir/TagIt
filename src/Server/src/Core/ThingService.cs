using TagIt.Connectors;
using TagIt.Store;
namespace TagIt;

public class ThingService : IThingService
{
    private readonly IEntityManager<Thing> _entityManager;
    private readonly IThingTypeStore _typeStore;
    private readonly IThingStore _thingStore;
    private readonly IConnectorFactory _connectorFactory;
    private readonly ILabelGeneratorService _labelGeneratorService;

    public ThingService(
        IEntityManager<Thing> entityManager,
        IThingTypeStore typeStore,
        IThingStore thingStore,
        IConnectorFactory connectorFactory,
        ILabelGeneratorService labelGeneratorService)
    {
        _entityManager = entityManager;
        _typeStore = typeStore;
        _thingStore = thingStore;
        _connectorFactory = connectorFactory;
        _labelGeneratorService = labelGeneratorService;
    }

    public async Task AddThingAsync(
        AddThingRequest request,
        CancellationToken cancellationToken)
    {
        Thing thing = _entityManager.CreateNew();
        thing.State = ThingState.Draft;
        thing.Title = request.Title;
        thing.Date = request.Date;
        thing.Label = request.Label;
        thing.TypeId = request.TypeId;
        thing.ClassId = request.ClassId;

        await ResolveLabelAsync(thing, cancellationToken);
        await ResolveTypeAsync(request, thing, cancellationToken);

        thing.Data = (await ProcessDataAsync(request, cancellationToken)).ToList();

        SaveEntityResult<Thing> saveResult = await _entityManager
            .SaveAsync(thing, cancellationToken);
    }

    public Task<IQueryable<Thing>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_thingStore.Query());
    }

    private async Task ResolveTypeAsync(AddThingRequest request, Thing thing, CancellationToken cancellationToken)
    {
        if (thing.TypeId is null && request.Type is { })
        {
            ThingType? type = await _typeStore.GetByTypeMapAsync(
                request.Type,
                cancellationToken);

            if (type is { })
            {
                thing.TypeId = type.Id;
            }
        }
    }

    private async Task ResolveLabelAsync(Thing thing, CancellationToken cancellationToken)
    {
        if (thing.Label is null)
        {
            thing.Label = await _labelGeneratorService.CreateLabelAsync(
                thing,
                cancellationToken);
        }
    }

    private async Task<IEnumerable<ThingDataReference>> ProcessDataAsync(
        AddThingRequest request,
        CancellationToken cancellationToken)
    {
        var refs = new List<ThingDataReference>();

        foreach (ThingData data in request.Data)
        {
            if (request.Action.Mode == JobActionMode.Import)
            {
                ThingDataReference dataRef = await HandleImportItem(
                    request,
                    data,
                    cancellationToken);

                refs.Add(dataRef);
            }
        }

        return refs;
    }

    private async Task<ThingDataReference> HandleImportItem(
        AddThingRequest request,
        ThingData data,
        CancellationToken cancellationToken)
    {
        var dataRef = new ThingDataReference
        {
            Type = data.Type,
            ConnectorId = request.Action.DestinationConnectorId!.Value
        };

        Stream? sourceStream = null;
        IConnector? connector = null;

        if (data.Data.Length > 0)
        {
            sourceStream = new MemoryStream(data.Data);
        }
        else
        {
            connector = await _connectorFactory.CreateAsync(
                data.ConnectorId, cancellationToken);

            sourceStream = await connector.DownloadAsync(
                data.Id,
                cancellationToken);
        }

        IConnector destinationConnecor = await _connectorFactory.CreateAsync(
            dataRef.ConnectorId,
            cancellationToken);


        dataRef.Location = await destinationConnecor.UploadAsync(
            $"{request.Title}.{request.Type}" ,
            sourceStream,
            cancellationToken);

        if (request.Action.Source.Mode == SourceActionMode.Delete && connector is { })
        {
            sourceStream.Close();

            await connector.DeleteAsync(data.Id, cancellationToken);
        }

        if (request.Action.Source.Mode == SourceActionMode.Move)
        {
            if (request.Action.Source.NewConnectorId.HasValue &&
                data.ConnectorId != request.Action.Source.NewConnectorId)
            {
                //Create and delete
                sourceStream.Seek(0, SeekOrigin.Begin);

                IConnector moveConnector = await _connectorFactory.CreateAsync(
                    request.Action.Source.NewConnectorId!.Value,
                    cancellationToken);

                var newPath = GetNewPath(request, data);

                await moveConnector.UploadAsync(newPath, sourceStream, cancellationToken);
                sourceStream.Close();

                await connector.DeleteAsync(data.Id, cancellationToken);
            }
            else
            {
                sourceStream.Close();

                await connector.MoveAsync(
                    data.Id,
                    request.Action.Source.NewLocation,
                    cancellationToken);
            }
        }

        sourceStream.Dispose();

        return dataRef;
    }

    private static string GetNewPath(AddThingRequest request, ThingData data)
    {
        var newPath = data.Location;
        if (request.Action.Source.NewLocation is { })
        {
            newPath = string.Join(
                '/',
                new[] { request.Action.Source.NewLocation, newPath });
        }

        return newPath;
    }

    public Task<Thing?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _thingStore.GetByIdAsync(id, cancellationToken)!;
    }
}
