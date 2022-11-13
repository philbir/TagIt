
namespace TagIt;

public interface IThingService
{
    Task<Thing?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IQueryable<Thing>> Query(CancellationToken cancellationToken);
}

