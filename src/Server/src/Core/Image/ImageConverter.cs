using ImageMagick;

namespace TagIt.Image;

public class MagickImageConverter
{
    public async Task<Stream> ConvertAsync(
        Stream stream,
        GenerateImageOptions options,
        CancellationToken cancellationToken)
    {
        using (MagickImage image = new MagickImage(stream))
        {
            image.Quality = options.Quality;
            image.Format = GetFormat(options.Format);

            if (options.Size is { } size)
            {
                image.Resize(size.Width, size.Height);
            }

            using var ms = new MemoryStream();
            await image.WriteAsync(ms, cancellationToken);

            return ms;
        }
    }

    private MagickFormat GetFormat(ImageFormat format)
    {
        switch (format)
        {
            case ImageFormat.WebP:
                return MagickFormat.WebP;
            case ImageFormat.Png:
                return MagickFormat.Png;
            default:
                throw new NotSupportedException(
                    $"Format: {format} is not supported");
        }
    }
}
