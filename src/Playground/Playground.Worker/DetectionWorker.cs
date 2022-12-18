using Serilog;

namespace TagIt;

public class DetectionWorker : BackgroundService
{
    private readonly IThingService _thingService;
    private readonly ICorrespondentService _correspondentService;
    private readonly IThingContentService _contentService;

    public DetectionWorker(
        IThingService thingService,
        ICorrespondentService correspondentService,
        IThingContentService contentService)
    {
        _thingService = thingService;
        _correspondentService = correspondentService;
        _contentService = contentService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Log.Information("Start detection");

        Thing thing = await _thingService.GetByIdAsync(
            Guid.Parse("90b7a2e6-9982-472e-9e9f-5ca09e00bb1c"),
            stoppingToken);

        var content = await _contentService.GetByThingIdAsync(thing.Id, stoppingToken);

        var correspondent = await _correspondentService.DetectFromContentAsync(content, stoppingToken);
    }
}
