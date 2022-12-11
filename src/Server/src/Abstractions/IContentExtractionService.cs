namespace TagIt;

public interface IContentExtractionService
{
    Task<IReadOnlyList<IThingContentData>> ExtractAsync(Thing thing, CancellationToken cancellationToken);
}
