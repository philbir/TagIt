using Microsoft.Extensions.DependencyInjection;

namespace TagIt.Pdf;

public static class PdfServiceCollectionExtensions
{
    public static IServiceCollection AddPdfImageConversion(
        this IServiceCollection services)
    {
        services.AddOptions<PdfOptions>()
            .BindConfiguration(Config.Pdf);

        services.AddSingleton<IImageExtractor, MagickPdfImageExtractor>();

        return services;
    }
}
