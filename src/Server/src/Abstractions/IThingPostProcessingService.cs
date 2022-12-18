namespace TagIt.Processing;

public interface IThingPostProcessingService
{
    Task UpdateThumbnailsAsync(Guid thingId, CancellationToken cancellationToken);
    Task DetectPropertiesAsync(Guid thingId, CancellationToken cancellationToken);
}
