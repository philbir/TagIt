namespace TagIt.Store;

public interface IPropertyDefinitionStore
{
    Task<PropertyDefinition> SaveAsync(
        PropertyDefinition entity,
        CancellationToken cancellationToken);

    Task<PropertyDefinition> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<PropertyDefinition>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IReadOnlyList<PropertyDefinition>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken);

    Task<PropertyDefinition> InsertAsync(PropertyDefinition entity, CancellationToken cancellationToken);

    Task<PropertyDefinition> UpdateAsync(PropertyDefinition entity, CancellationToken cancellationToken);

    public IQueryable<PropertyDefinition> Query();
}




