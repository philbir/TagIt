using TagIt.Image;

namespace TagIt.Tests;

[UsesVerify]
public class MagickImageConverterTests
{
    public MagickImageConverterTests()
    {
        VerifyImageMagick.Initialize();
        VerifyImageMagick.RegisterComparers(0.05);

        DerivePathInfo(
            (sourceFile, projectDirectory, type, method) => new(
                directory: Path.Combine(projectDirectory, "__verify__"),
                typeName: type.Name,
                methodName: method.Name));
    }
    [Fact]
    public async Task ConvertPngToWegPWithResize_ImageEqual()
    {
        // Arrange
        Stream image = TestSources.Image.SmallPng;
        var options = new GenerateImageOptions
        {
            Format = ImageFormat.WebP,
            Size = new ImageSize { Height = 120, Width = 50 }
        };

        // Act
        var service = new MagickImageConverter();

        Stream converted = await service.ConvertAsync(
            image, options, CancellationToken.None);

        // Assert
        var data = converted.ToByteArray();

        await Verify(data, "webp");
    }
}

