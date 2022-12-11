using Serilog;

namespace TagIt;

public class DataExtractionWorker : BackgroundService
{
    private readonly IThingService _thingService;
    private readonly IContentExtractionService _contentExtractionService;

    public DataExtractionWorker(
        IThingService thingService,
        IContentExtractionService contentExtractionService)
    {
        _thingService = thingService;
        _contentExtractionService = contentExtractionService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Log.Information("Start Data Extraction");

        Thing thing = await _thingService.GetByIdAsync(
            Guid.Parse("90b7a2e6-9982-472e-9e9f-5ca09e00bb1c"),
            stoppingToken);

        var data = await _contentExtractionService.ExtractAsync(thing, stoppingToken);

        var all = string.Join('\n', data.Select(x =>
        {
            return x.ToString();
        }));

        var dateExtractor = new DateTokenExtractor();

        var tokens = new List<TokenData>();


        foreach (var text in all.Split('\n'))
        {
            var res = await dateExtractor.TokenizeAsync(text, stoppingToken);
            tokens.AddRange(res);
        }

        Console.ReadLine();
    }
}
