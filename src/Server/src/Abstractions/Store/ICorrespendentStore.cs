namespace TagIt.Store;

public interface ICorrespendentStore
{
    Task<Correspondent> InsertAsync(Correspondent entity, CancellationToken cancellationToken);

    Task<Correspondent> UpdateAsync(Correspondent entity, CancellationToken cancellationToken);

    Task<Correspondent> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Correspondent>> GetManyAsync(
        IEnumerable<Guid> ids, CancellationToken
        cancellationToken);

    public IQueryable<Correspondent> Query();

}
