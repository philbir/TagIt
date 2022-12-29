namespace TagIt;

public class ContentExtractionService : IContentExtractionService
{
    private readonly IThingDataService _thingDataService;
    private readonly IEnumerable<IThingContentExtractor> _thingDataExtractors;

    public ContentExtractionService(
        IThingDataService thingDataService,
        IEnumerable<IThingContentExtractor> thingDataExtractors)
    {
        _thingDataService = thingDataService;
        _thingDataExtractors = thingDataExtractors;
    }

    public async Task<IReadOnlyList<IThingContentData>> ExtractAsync(Thing thing, CancellationToken cancellationToken)
    {
        // TOD: Define which extractors to use based on thing
        IEnumerable<IThingContentExtractor> extractors = _thingDataExtractors;

        // Build Context
        var context = new ThingContentExtractionContext(thing);
        context.ArchivedData = await _thingDataService.GetPdfArchiveAsync(thing, cancellationToken);

        var result = new List<IThingContentData>();

        foreach (IThingContentExtractor extractor in extractors)
        {
            IReadOnlyList<IThingContentData> data = await extractor.ExtractAsync(context, cancellationToken);
            result.AddRange(data);
        }

        return result;
    }
}
