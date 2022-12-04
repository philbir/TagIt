namespace TagIt;

public interface ITagDefinitionService
{
    Task<TagDefinition> AddAsync(string name, CancellationToken cancellationToken);
    Task<TagDefinition> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TagDefinition>> GetManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<IQueryable<TagDefinition>> Query(CancellationToken cancellationToken);
}