namespace TagIt.Store;

public interface IReceiverStore
{
    Task<Receiver> InsertAsync(Receiver entity, CancellationToken cancellationToken);

    Task<Receiver> UpdateAsync(Receiver entity, CancellationToken cancellationToken);

    Task<Receiver> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Receiver>> GetManyAsync(
        IEnumerable<Guid> ids, CancellationToken
        cancellationToken);

    Task<IReadOnlyList<Receiver>> GetAllAsync(CancellationToken cancellationToken);

    public IQueryable<Receiver> Query();

}
