using HotChocolate.Types.Relay;
using TagIt.Store;

namespace TagIt;

public class ThingService : IThingService
{
    private readonly IThingStore _thingStore;
    private readonly IEntityManager<Thing> _entityManager;

    public ThingService(
        IThingStore thingStore,
        IEntityManager<Thing> entityManager)
    {
        _thingStore = thingStore;
        _entityManager = entityManager;
    }

    public Task<IQueryable<Thing>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_thingStore.Query());
    }
    public Task<Thing> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _thingStore.GetByIdAsync(id, cancellationToken)!;
    }

    public Task UpdateThumbnailsAsync(
        Guid id,
        List<ThingThumbnail> thumbails,
        CancellationToken cancellationToken)
            => _thingStore.UpdateThumbnailsAsync(id, thumbails, cancellationToken);

    public async Task<Thing> UpdateThingAsync(UpdateThingRequest request, CancellationToken cancellationToken)
    {
        Thing thing = await _entityManager.GetExistingOrCreateNewAsync(request.Id, cancellationToken);

        thing.TypeId = request.TypeId;
        thing.ClassId = request.ClassId;
        thing.Title = request.Title;
        thing.ReceiverId = request.ReceiverId;
        thing.CorespondentId = request.CorrespondentId;

        UpdateProperties(thing, request);

        SaveEntityResult<Thing> result = await _entityManager.SaveAsync(thing, cancellationToken);

        return result.Entity;
    }

    private static void UpdateProperties(Thing thing, UpdateThingRequest request)
    {
        var properties = thing.Properties.ToList();

        foreach (UpdateThingPropertyRequest property in request.Properties)
        {
            if (property.Id.HasValue)
            {
                ThingPropery? existing = properties.FirstOrDefault(x => x.Id == property.Id);

                if (existing is { })
                {
                    existing.Value = property.Value;
                }
            }
            else
            {
                properties.Add(new ThingPropery
                {
                    Id = Guid.NewGuid(),
                    DefinitionId = property.DefinitionId,
                    Value = property.Value,
                });
            }
        }

        thing.Properties = properties;
    }
}

