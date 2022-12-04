using Serilog;

namespace TagIt;

public class DataExtractionWorker : BackgroundService
{
    private readonly IThingService _thingService;
    private readonly IDataExtractionService _dataExtractionService;

    public DataExtractionWorker(
        IThingService thingService,
        IDataExtractionService dataExtractionService)
    {
        _thingService = thingService;
        _dataExtractionService = dataExtractionService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Log.Information("Start Data Extraction");

        Thing thing = await _thingService.GetByIdAsync(
            Guid.Parse("90b7a2e6-9982-472e-9e9f-5ca09e00bb1c"),
            stoppingToken);

        var data = await _dataExtractionService.ExtractAsync(thing, stoppingToken);

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
