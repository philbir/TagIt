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

public interface IPdfOcrService
{
    Task<CreatePdfResult> CreatePdfAsync(CreatePdfRequest request, CancellationToken cancellationToken);
}

public record CreatePdfRequest(string Filename, Stream Stream);

public record CreatePdfResult(byte[] Data);
