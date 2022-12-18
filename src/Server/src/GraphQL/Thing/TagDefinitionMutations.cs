namespace TagIt.GraphQL;

[MutationType]
public class TagDefinitionMutations
{
    public Task<TagDefinition> AddTagDefintionAsync(
        [Service] ITagDefinitionService tagDefinitionService,
        string name, CancellationToken cancellationToken)
    {
        return tagDefinitionService.AddAsync(name, cancellationToken);
    }
}


