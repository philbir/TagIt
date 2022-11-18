namespace TagIt;

public class PdfImageExtractor
{
    public string[] SupportedContentTypes { get; } = new[] { "pdf" };

    public Task<IEnumerable<ImageData>> ExtractAsync(
        Thing thing,
        CancellationToken cancellationToken)
    {
        return null;
    }
}
