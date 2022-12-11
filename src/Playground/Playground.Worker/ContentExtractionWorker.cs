using Serilog;

namespace TagIt;

public class ContentExtractionWorker : BackgroundService
{
    private readonly IThingService _thingService;
    private readonly IContentExtractionService _contentExtractionService;
    private readonly IThingContentService _contentService;

    public ContentExtractionWorker(
        IThingService thingService,
        IContentExtractionService contentExtractionService,
        IThingContentService contentService)
    {
        _thingService = thingService;
        _contentExtractionService = contentExtractionService;
        _contentService = contentService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Log.Information("Start Data Extraction");

        Thing thing = await _thingService.GetByIdAsync(
            Guid.Parse("90b7a2e6-9982-472e-9e9f-5ca09e00bb1c"),
            stoppingToken);

        IReadOnlyList<IThingContentData> contents = await _contentExtractionService.ExtractAsync(
            thing,
            stoppingToken);

        await _contentService.AddContentAsync(thing.Id, contents, stoppingToken);

        var all = string.Join('\n', contents.Select(x =>
        {
            return x.ToString();
        }));

        var dateExtractor = new DateContentTokenizer();

        var tokens = new List<TokenData>();

        foreach (var text in all.Split('\n'))
        {
            var res = await dateExtractor.TokenizeAsync(text, stoppingToken);
            tokens.AddRange(res);
        }

        Console.ReadLine();
    }
}
