using TagIt.Store;

namespace TagIt;

public class TagDefinitionService : ITagDefinitionService
{
    private readonly ITagDefinitionStore _store;

    public TagDefinitionService(ITagDefinitionStore store)
    {
        _store = store;
    }

    public Task<IQueryable<TagDefinition>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.Query());
    }

    public Task<TagDefinition> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _store.GetByIdAsync(id, cancellationToken)!;
    }

    public async Task<TagDefinition> AddAsync(string name, CancellationToken cancellationToken)
    {
        var tagDefinition = new TagDefinition()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Color = ColorUtils.GetRandomHexColor(),
        };

        return await _store.InsertAsync(tagDefinition, cancellationToken);
    }

    public Task<IReadOnlyList<TagDefinition>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken)
         => _store.GetManyAsync(ids, cancellationToken);
}
