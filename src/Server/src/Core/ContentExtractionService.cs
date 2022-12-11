namespace TagIt;

public class ContentExtractionService : IContentExtractionService
{
    private readonly IThingDataResolver _thingDataResolver;
    private readonly IEnumerable<IThingContentExtractor> _thingDataExtractors;

    public ContentExtractionService(
        IThingDataResolver thingDataResolver,
        IEnumerable<IThingContentExtractor> thingDataExtractors)
    {
        _thingDataResolver = thingDataResolver;
        _thingDataExtractors = thingDataExtractors;
    }

    public async Task<IReadOnlyList<IThingContentData>> ExtractAsync(Thing thing, CancellationToken cancellationToken)
    {
        // TOD: Define which extractors to use based on thing
        IEnumerable<IThingContentExtractor> extractors = _thingDataExtractors;

        // Build Context
        var context = new ThingContentExtractionContext(thing);
        context.ArchivedData = await _thingDataResolver.GetOriginalAsync(thing, cancellationToken);

        var result = new List<IThingContentData>();

        foreach (IThingContentExtractor extractor in extractors)
        {
            IReadOnlyList<IThingContentData> data = await extractor.ExtractAsync(context, cancellationToken);
            result.AddRange(data);
        }

        return result;
    }
}



public class MatchRule
{
    public MatchRuleType Type { get; set; }

    public string Expression { get; set; }

    public int Weight { get; set; }

    public string Field { get; set; }
}

public enum MatchRuleType
{
    Regex
}
