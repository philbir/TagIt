using TagIt;

namespace Playground.Worker;

public class TagItWorker : BackgroundService
{
    private readonly ILogger<TagItWorker> _logger;
    private readonly ThingService _thingService;

    public TagItWorker(ILogger<TagItWorker> logger, ThingService thingService)
    {
        _logger = logger;
        _thingService = thingService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var addThingRequest = new AddThingRequest
        {
            Title = "A test file",
            Date = DateTime.Now,
            Data = new List<ThingData>
            {
                new ThingData
                {
                    ConnectorId = Guid.Parse("8f05f1c0-78e0-42d4-ad0a-beacffc0db8f"),
                    Location = "Proposal__2022-09-20.pdf"
                },
            }
        };

        await _thingService.AddThingAsync(addThingRequest, stoppingToken);
    }
}
