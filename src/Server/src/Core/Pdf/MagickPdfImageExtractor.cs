using ImageMagick;
using Microsoft.Extensions.Options;

namespace TagIt.Pdf;

public class MagickPdfImageExtractor : IImageExtractor
{
    private readonly PdfOptions _options;

    public MagickPdfImageExtractor(IOptions<PdfOptions> options)
    {
        _options = options.Value;
        //MagickNET.SetGhostscriptDirectory(_options.GhostScriptPath);
    }

    public string[] SupportedTypes => new string[] { "pdf" };

    public async Task<Stream> ExtractAsync(
        Stream pdfStream,
        int pageNumber,
        CancellationToken cancellationToken)
    {
        MagickReadSettings settings = new MagickReadSettings();
        settings.Density = new Density(300, 300);
        using var images = new MagickImageCollection();

        images.Read(pdfStream, settings);

        var imageStream = new MemoryStream();

        await images[pageNumber - 1].WriteAsync(
            imageStream,
            MagickFormat.Png,
            cancellationToken);

        return imageStream;
    }

    public Task<Stream> ExtractAsync(
        Stream stream,
        ExtractImageOptions options, CancellationToken cancellationToken)
    {
        return ExtractAsync(stream, options.PageNumber, cancellationToken);
    }
}
