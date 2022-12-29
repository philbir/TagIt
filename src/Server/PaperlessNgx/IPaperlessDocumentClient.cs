namespace TagIt.PaperlessNgx;

public interface IPaperlessDocumentClient
{
    Task<CreatePdfResult> CreatePdfAsync(CreatePdfRequest request, CancellationToken cancellationToken);
}
