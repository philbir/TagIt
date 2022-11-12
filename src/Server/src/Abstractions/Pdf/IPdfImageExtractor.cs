namespace TagIt.Pdf;

public interface IPdfImageExtractor
{
    Task<Stream> ExtractAsync(
            Stream pdfStream,
            int pageNr,
            CancellationToken cancellationToken);
}
