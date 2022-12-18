using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace TagIt;

public class PdfTextContentExtractor : IThingContentExtractor
{
    public string Name => "PdfText";

    public Task<IReadOnlyList<IThingContentData>> ExtractAsync(
        ThingContentExtractionContext context,
        CancellationToken cancellationToken)
    {
        var reader = new PdfReader(context.ArchivedData.Stream);

        PdfDocument pdf = new (reader);
        int pages = pdf.GetNumberOfPages();

        var result = new List<IThingContentData>();

        for (var i = 0; i < pages; i++)
        {
            PdfPage page = pdf.GetPage(i + 1);

            string? text = PdfTextExtractor.GetTextFromPage(
                page,
                new SimpleTextExtractionStrategy());

            var content = new PageTextContent
            {
                PageNumber = i + 1,
                Lines = text.Split('\n'),
                Source = Name
            };

            result.Add(content);
        }

        return Task.FromResult((IReadOnlyList<IThingContentData>) result);
    }
}
