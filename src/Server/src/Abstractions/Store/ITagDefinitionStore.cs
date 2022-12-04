namespace TagIt.Store;

public interface ITagDefinitionStore
{
    Task<TagDefinition> SaveAsync(
        TagDefinition entity,
        CancellationToken cancellationToken);

    Task<TagDefinition> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<TagDefinition>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IReadOnlyList<TagDefinition>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken);

    Task<TagDefinition> InsertAsync(
        TagDefinition entity,
        CancellationToken cancellationToken);

    Task<TagDefinition> UpdateAsync(
        TagDefinition entity,
        CancellationToken cancellationToken);

    public IQueryable<TagDefinition> Query();
}
