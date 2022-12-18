namespace TagIt;

public interface IThingContentExtractor
{
    public string Name { get; }

    public Task<IReadOnlyList<IThingContentData>> ExtractAsync(
        ThingContentExtractionContext context,
        CancellationToken cancellationToken);
}
