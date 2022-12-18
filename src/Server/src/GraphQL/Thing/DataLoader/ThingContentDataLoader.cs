namespace TagIt.GraphQL;

public class ThingContentDataLoader : CacheDataLoader<Guid, ThingContentAccessor>
{
    private readonly IThingContentService _contentService;

    public ThingContentDataLoader(IThingContentService contentService)
    {
        _contentService = contentService;
    }

    protected override Task<ThingContentAccessor> LoadSingleAsync(Guid key, CancellationToken cancellationToken)
    {
        return _contentService.GetByThingIdAsync(key, cancellationToken);
    }
}
