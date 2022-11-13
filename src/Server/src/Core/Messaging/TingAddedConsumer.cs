using MassTransit;
using Serilog;
namespace TagIt.Messaging;

public class ThingAddedConsumer : IConsumer<ThingAddedMessage>
{
    public Task Consume(ConsumeContext<ThingAddedMessage> context)
    {
        Log.Information("Consume ThingAdded: {Id}", context.Message.Id);

        // Start Post-Processing
        // - Thumbnails
        // - OCR
        // - DataExtraction
        // - Classifier
        // - Actions 

        return Task.CompletedTask;
    }
}
