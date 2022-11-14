
namespace TagIt;

public interface IThingService
{
    Task<Thing> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IQueryable<Thing>> Query(CancellationToken cancellationToken);
    Task UpdateThumbnailsAsync(Guid id, List<ThingThumbnail> thumbails, CancellationToken cancellationToken);
}

