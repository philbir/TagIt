namespace TagIt.Store.Mongo;

public class PropertyDefinitionStore : Store<PropertyDefinition>, IPropertyDefinitionStore
{
    public PropertyDefinitionStore(ITagIdDbContext dbContext)
        : base(dbContext)
    {
    }

    IQueryable<PropertyDefinition> IPropertyDefinitionStore.Query()
    {
        return Query;
    }
}
