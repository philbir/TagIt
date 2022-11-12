using System.IO;
using TagIt.Pdf;

namespace TagIt.Tests;

public class MuPdfImageExtractorTests : VerifyImageTest
{
    [Fact]
    public async Task Extract()
    {
        // Arrange
        Stream pdf = TestSources.Pdf.Simple2Page;

        // Act
        var service = new MuPdfImageExtractor();

        Stream image = await service
            .ExtractAsync(pdf, 1, TestCanceled);

        // Assert
        var data = image.ToByteArray();

        await Verify(data, "png");
    }
}

