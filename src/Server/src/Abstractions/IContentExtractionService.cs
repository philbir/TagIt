namespace TagIt;

public interface IContentExtractionService
{
    Task<IReadOnlyList<IThingContentData>> ExtractAsync(Thing thing, CancellationToken cancellationToken);
}

public class PageTextContent : ExtractedContent, IThingContentData
{
    public int PageNumber { get; set; }

    public IReadOnlyList<string> Lines { get; set; } = Array.Empty<string>();

    public override string ToString()
    {
        return string.Join('\n', Lines);
    }
}

public class ExtractedContent
{
    public string Source { get; set; }
}
