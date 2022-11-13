using MassTransit;
using Serilog;
namespace TagIt.Messaging;

public class ThingAddedConsumer : IConsumer<ThingAddedMessage>
{
    private readonly IThumbnailGeneratorService _thumbnailGeneratorService;

    public ThingAddedConsumer(IThumbnailGeneratorService thumbnailGeneratorService)
    {
        _thumbnailGeneratorService = thumbnailGeneratorService;
    }

    public async Task Consume(ConsumeContext<ThingAddedMessage> context)
    {
        Log.Information("Consume ThingAdded: {Id}", context.Message.Id);

        // Start Post-Processing
        // - Thumbnails
        // - OCR
        // - DataExtraction
        // - Classifier
        // - Actions

        await _thumbnailGeneratorService.UpdateThumbnailsAsync(context.Message.Id, context.CancellationToken);
    }
}
