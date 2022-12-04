
namespace TagIt.GraphQL;

public class TagDefinitionByIdDataLoader
    : BatchDataLoader<Guid, TagDefinition>
{
    private readonly ITagDefinitionService _service;

    public TagDefinitionByIdDataLoader(
        ITagDefinitionService service,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _service = service;
    }

    protected override async Task<IReadOnlyDictionary<Guid, TagDefinition?>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        IEnumerable<TagDefinition> definitions =
            await _service.GetManyAsync(keys, cancellationToken);

        return definitions.ToDictionary(x => x.Id)!;
    }
}

