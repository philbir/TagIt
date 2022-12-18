namespace TagIt;

public interface IContentTokenizerService
{
    ValueTask<IReadOnlyList<ContentTokenData>> TokenizeAsync(
        string value,
        CancellationToken cancellationToken);
}

public interface IContentTokenizer
{
    string Name { get; }
    ValueTask<IReadOnlyList<ContentTokenData>> TokenizeAsync(
        string value,
        CancellationToken cancellationToken);
}

public class ContentTokenData
{
    public string Tokenizer { get; set; }
    public string Value { get; set; }
    public List<string> Displays { get; set; } = new List<string>();
    public int MatchCount { get; set; }

    public override string ToString()
    {
        return Value?.ToString();
    }
}

