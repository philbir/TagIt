namespace TagIt;

public interface IImageExtractor
{
    Task<Stream> ExtractAsync(
        Stream stream,
        ExtractImageOptions options,
        CancellationToken cancellationToken);

    string[] SupportedTypes { get; }
}

public class ExtractImageOptions
{
    public int PageNumber { get; set; } = 1;
}
