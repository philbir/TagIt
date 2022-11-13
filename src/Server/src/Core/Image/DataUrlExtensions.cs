namespace TagIt.Image;

public static class DataUrlExtensions
{
    public static string ToImageDataUrl(this byte[] image, ImageFormat format)
    {
        return $"data:image/{format.ToString().ToLowerInvariant()};base64,{Convert.ToBase64String(image)}";
    }
}
