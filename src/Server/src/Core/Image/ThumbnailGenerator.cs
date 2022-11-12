namespace TagIt.Image;

public class ThumbnailGenerator
{
    public async Task<Thumbnail> GenerateAsync(
        Stream stream,
        ImageSize size,
        ImageFormat format)
    {
        throw new NotImplementedException();
    }

    private async Task<Stream> GenerateImageAsync(
        Stream stream,
        CancellationToken cancellationToken)
    {

        return null;

    }
}

public class GenerateImageOptions
{
    public ImageSize? Size { get; set; }

    public int Quality { get; set; } = 100;

    public ImageFormat Format { get; set; } = ImageFormat.WebP;
}

