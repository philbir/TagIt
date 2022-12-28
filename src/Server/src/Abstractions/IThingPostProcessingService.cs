namespace TagIt.Processing;

public interface IThingPostProcessingService
{
    Task UpdateContentAsync(Guid thingId, CancellationToken cancellationToken);
    Task DetectPropertiesAsync(Guid thingId, CancellationToken cancellationToken);
}
