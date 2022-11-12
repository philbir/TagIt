using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TagIt.Pdf;
public class PdfOptions
{
    public string GhostScriptPath { get; set; }

    public ImageFormat ThumbnailImageFormat { get; set; }
}


public static class PdfServiceCollectionExtensions
{
    public static IServiceCollection AddPdfImageConversion(
        this IServiceCollection services)
    {


        services.AddSingleton<IPdfImageExtractor, MagickPdfImageExtractor>();

        return services;

    }
}
