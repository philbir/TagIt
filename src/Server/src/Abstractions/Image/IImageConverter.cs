namespace TagIt.Image;

public interface IImageConverter
{
    Task<ImageData> ConvertAsync(
        Stream stream,
        ConvertImageOptions options,
        CancellationToken cancellationToken);
}

public class ConvertImageOptions
{
    public ImageSize? Size { get; set; }

    public int Quality { get; set; } = 100;

    public ImageFormat Format { get; set; } = ImageFormat.WebP;
}
