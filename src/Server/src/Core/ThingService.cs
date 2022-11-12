using TagIt.Connectors;
using TagIt.Messaging;
using TagIt.Store;
using static HotChocolate.ErrorCodes;

namespace TagIt;

public class ThingService : IThingService
{
    private readonly IEntityManager<Thing> _entityManager;
    private readonly IThingTypeStore _typeStore;
    private readonly IThingStore _thingStore;
    private readonly IConnectorFactory _connectorFactory;
    private readonly ILabelGeneratorService _labelGeneratorService;
    private readonly IMessagePublisher _messagePublisher;

    public ThingService(
        IEntityManager<Thing> entityManager,
        IThingTypeStore typeStore,
        IThingStore thingStore,
        IConnectorFactory connectorFactory,
        ILabelGeneratorService labelGeneratorService,
        IMessagePublisher messagePublisher)
    {
        _entityManager = entityManager;
        _typeStore = typeStore;
        _thingStore = thingStore;
        _connectorFactory = connectorFactory;
        _labelGeneratorService = labelGeneratorService;
        _messagePublisher = messagePublisher;
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
        thing.Source = request.Source;

        await ResolveLabelAsync(thing, cancellationToken);
        await ResolveTypeAsync(request, thing, cancellationToken);

        ThingDataReference data = await SaveOriginalAsync(request, cancellationToken);

        thing.Data = new List<ThingDataReference>() { data };

        //thing.Data = (await ProcessDataAsync(request, cancellationToken)).ToList();

        SaveEntityResult<Thing> saveResult = await _entityManager
            .SaveAsync(thing, cancellationToken);

        await _messagePublisher
            .PublishAsync(new ThingAddedMessage(saveResult.Entity.Id), cancellationToken);
    }

    private async Task<ThingDataReference> SaveOriginalAsync(
        AddThingRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Action.Mode == JobActionMode.Import)
        {
            ThingDataReference dataRef = await HandleImportItem(
                request,
                cancellationToken);

            return dataRef;
        }
        throw new NotImplementedException();
    }

    public Task<IQueryable<Thing>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_thingStore.Query());
    }

    private async Task ResolveTypeAsync(
        AddThingRequest request,
        Thing thing,
        CancellationToken cancellationToken)
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

        foreach (ThingData data in request.AdditionalData)
        {
            if (request.Action.Mode == JobActionMode.Import)
            {
                ThingDataReference dataRef = await HandleImportItem(
                    request,
                    cancellationToken);

                refs.Add(dataRef);
            }
        }

        return refs;
    }

    private async Task<ThingDataReference> HandleImportItem(
        AddThingRequest request,
        CancellationToken cancellationToken)
    {
        var dataRef = new ThingDataReference
        {
            ConnectorId = request.Action.DestinationConnectorId!.Value
        };

        IConnector connector = await _connectorFactory.CreateAsync(
                request.Source.ConnectorId,
                cancellationToken);

        Stream sourceStream = await connector.DownloadAsync(
            request.Source.Id,
            cancellationToken);

        IConnector destinationConnecor = await _connectorFactory.CreateAsync(
            dataRef.ConnectorId,
            cancellationToken);

        dataRef.Id = await destinationConnecor.UploadAsync(
            $"{request.Title}.{request.Type}",
            sourceStream,
            cancellationToken);

        if (request.Action.Source.Mode == SourceActionMode.Delete)
        {
            sourceStream.Close();

            await connector.DeleteAsync(request.Source.Id, cancellationToken);
        }

        if (request.Action.Source.Mode == SourceActionMode.Move)
        {
            if (request.Action.Source.NewConnectorId.HasValue &&
                request.Source.ConnectorId != request.Action.Source.NewConnectorId)
            {
                //Create and delete
                sourceStream.Seek(0, SeekOrigin.Begin);

                IConnector moveConnector = await _connectorFactory.CreateAsync(
                    request.Action.Source.NewConnectorId!.Value,
                    cancellationToken);

                var newPath = GetNewPath(request, request.Source.Id);

                await moveConnector.UploadAsync(newPath, sourceStream, cancellationToken);
                sourceStream.Close();

                await connector.DeleteAsync(request.Source.Id, cancellationToken);
            }
            else
            {
                sourceStream.Close();

                await connector.MoveAsync(
                    request.Source.Id,
                    request.Action.Source.NewLocation,
                    cancellationToken);
            }
        }

        sourceStream.Dispose();

        return dataRef;
    }

    private static string GetNewPath(AddThingRequest request, string id)
    {
        var newPath = id;

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
