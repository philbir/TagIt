using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace TagIt;

public class DataContent : IDataExtractionService
{
    private readonly IThingDataResolver _thingDataResolver;
    private readonly IEnumerable<IThingDataExtractor> _thingDataExtractors;

    public DataContent(
        IThingDataResolver thingDataResolver,
        IEnumerable<IThingDataExtractor> thingDataExtractors)
    {
        _thingDataResolver = thingDataResolver;
        _thingDataExtractors = thingDataExtractors;
    }

    public async Task<IReadOnlyList<IExtractedData>> ExtractAsync(Thing thing, CancellationToken cancellationToken)
    {
        // TOD: Define which extractors to use based on thing
        IEnumerable<IThingDataExtractor> extractors = _thingDataExtractors;

        // Build Context
        var context = new ThingDataExtractionContext(thing);
        context.ArchivedData = await _thingDataResolver.GetOriginalAsync(thing, cancellationToken);

        var result = new List<IExtractedData>();

        foreach (IThingDataExtractor extractor in extractors)
        {
            IReadOnlyList<IExtractedData> data = await extractor.ExtractAsync(context, cancellationToken);
            result.AddRange(data);
        }

        return result;
    }
}

public class PdfTextDataExtractor : IThingDataExtractor
{
    public string Name => "PdfText";

    public Task<IReadOnlyList<IExtractedData>> ExtractAsync(
        ThingDataExtractionContext context,
        CancellationToken cancellationToken)
    {
        var reader = new PdfReader(context.ArchivedData.Stream);

        PdfDocument pdf = new PdfDocument(reader);
        int pages = pdf.GetNumberOfPages();

        var result = new List<IExtractedData>();

        for (var i = 0; i < pages; i++)
        {
            PdfPage page = pdf.GetPage(i + 1);

            var text = PdfTextExtractor.GetTextFromPage(page, new SimpleTextExtractionStrategy());

            var data = new TextData
            {
                PageNumber = i + 1,
                Lines = text.Split('\n'),
                Source = Name
            };

            result.Add(data);
        }

        return Task.FromResult((IReadOnlyList<IExtractedData>) result);
    }
}

public interface IThingDataExtractor
{
    public string Name { get; }

    public Task<IReadOnlyList<IExtractedData>> ExtractAsync(ThingDataExtractionContext context, CancellationToken cancellationToken);
}

public class TextData : ExtractedData, IExtractedData
{
    public int PageNumber { get; set; }

    public IReadOnlyList<string> Lines { get; set; } = Array.Empty<string>();

    public override string ToString()
    {
        return string.Join('\n', Lines);
    }
}

public class ExtractedData
{
    public string Source { get; set; }
}

public class ThingDataExtractionContext
{
    public ThingDataExtractionContext(Thing thing)
    {
        Thing = thing;
    }

    public Thing Thing { get; }

    public Stream Archived { get; set; }

    public ThingData ArchivedData { get; internal set; }
}

public class MatchRule
{
    public MatchRuleType Type { get; set; }

    public string Expression { get; set; }

    public int Weight { get; set; }
}

public enum MatchRuleType
{
    Regex
}
