using Microsoft.Extensions.Options;
using TagIt.Pdf;

namespace TagIt.Tests;

public class MagickPdfImageExtractorTests : VerifyImageTest
{
    [Fact]
    public async Task Extract()
    {
        // Arrange
        Stream pdf = TestSources.Pdf.Simple2Page;
        var pdfOptions = new PdfOptions
        {
            GhostScriptPath = @"C:\Program Files\gs\gs10.00.0\bin",
            ThumbnailImageFormat = ImageFormat.WebP
        };

        // Act
        var service = new MagickPdfImageExtractor(Options.Create(pdfOptions));

        Stream image = await service
            .ExtractAsync(pdf, 1, TestCanceled);

        // Assert
        var data = image.ToByteArray();
        await Verify(data, "png");
    }
}

