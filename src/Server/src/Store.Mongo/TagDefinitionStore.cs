namespace TagIt.Store.Mongo;

public class TagDefinitionStore : Store<TagDefinition>, ITagDefinitionStore
{
    public TagDefinitionStore(ITagIdDbContext dbContext)
        : base(dbContext)
    {
    }

    IQueryable<TagDefinition> ITagDefinitionStore.Query()
    {
        return Query;
    }
}
