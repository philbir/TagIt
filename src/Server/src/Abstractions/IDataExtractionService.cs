namespace TagIt;

public interface IDataExtractionService
{
    Task<IReadOnlyList<IExtractedData>> ExtractAsync(Thing thing, CancellationToken cancellationToken);
}