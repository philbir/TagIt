using ImageMagick;

namespace TagIt.Pdf;

public class MagickPdfImageExtractor : IPdfImageExtractor
{
   
    private readonly PdfOptions _options;

    public MagickPdfImageExtractor(PdfOptions options)
    {
        _options = options;
        MagickNET.SetGhostscriptDirectory(_options.GhostScriptPath);
    }

    public async Task<Stream> ExtractAsync(
        Stream pdfStream,
        int pageNr,
        CancellationToken cancellationToken)
    {

        MagickReadSettings settings = new MagickReadSettings();
        settings.Density = new Density(300, 300);
        using var images = new MagickImageCollection();

        images.Read(pdfStream, settings);

        var imageStream = new MemoryStream();
        await images[pageNr - 1].WriteAsync(
            imageStream,
            MagickFormat.Png,
            cancellationToken);

        return imageStream;
    }

}
