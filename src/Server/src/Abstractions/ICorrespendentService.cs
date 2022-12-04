namespace TagIt;

public interface ICorrespondentService
{
    Task<Correspondent> AddAsync(string name, CancellationToken cancellationToken);
    Task<Correspondent> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Correspondent>> GetManyAsync(
        IEnumerable<Guid> ids, CancellationToken
        cancellationToken);

    Task<IQueryable<Correspondent>> Query(CancellationToken cancellationToken);
}
