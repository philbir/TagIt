namespace TagIt;

public interface IReceiverService
{
    Task<Receiver> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Receiver>> GetManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<IQueryable<Receiver>> Query(CancellationToken cancellationToken);
}