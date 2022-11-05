
namespace TagIt;

public interface IThingService
{
    Task AddThingAsync(
        AddThingRequest request,
        CancellationToken cancellationToken);

    Task<Thing?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IQueryable<Thing>> Query(CancellationToken cancellationToken);
}

