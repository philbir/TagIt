using ImageMagick;

namespace TagIt.Image;

public class MagickImageConverter : IImageConverter
{
    public async Task<ImageData> ConvertAsync(
        Stream stream,
        ConvertImageOptions options,
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

            var ms = new MemoryStream();
            await image.WriteAsync(ms, cancellationToken);

            return new ImageData
            {
                Data = ms.ToArray(),
                Format = options.Format,
                Size = new ImageSize { Height = image.Height, Width = image.Width },
            };
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
