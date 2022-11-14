namespace TagIt;

public interface IThumbnailGeneratorService
{
    Task UpdateThumbnailsAsync(Guid id, CancellationToken cancellationToken);
}