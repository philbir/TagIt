using MuPDFCore;

namespace TagIt.Pdf;
public class MuPdfImageExtractor 
{
    public Task<Stream> ExtractAsync(
        Stream pdfStream,
        int pageNr,
        CancellationToken cancellationToken)
    {
        MuPDFContext? context = null;

        try
        {
            context = new MuPDFContext();
            using var imageMemoryStream = new MemoryStream();

            using var document =
                new MuPDFDocument(context, pdfStream.ToByteArray(), InputFileTypes.PDF);

            if (document.Pages.Count < pageNr)
            {
                return null;
            }

            document.WriteImage(
                pageNr - 1,
                1,
                PixelFormats.RGBA,
                imageMemoryStream,
                RasterOutputFileTypes.PNG,
                includeAnnotations: false);

            return Task.FromResult((Stream)imageMemoryStream);
        }
        catch (MuPDFException ex)
        {
            throw;
        }
        finally
        {
            context?.ClearStore();
            context?.Dispose();
        }
    }
}
