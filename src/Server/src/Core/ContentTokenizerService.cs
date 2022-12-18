using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TagIt;

public class ContentTokenizerService : IContentTokenizerService
{
    private readonly IEnumerable<IContentTokenizer> _tokenizers;

    public ContentTokenizerService(IEnumerable<IContentTokenizer> tokenizers)
    {
        _tokenizers = tokenizers;
    }

    public async ValueTask<IReadOnlyList<ContentTokenData>> TokenizeAsync(
        string value,
        CancellationToken cancellationToken)
    {
        var result = new List<ContentTokenData>();

        foreach (IContentTokenizer tokenizer in _tokenizers)
        {
            IReadOnlyList<ContentTokenData> tokens = await tokenizer.TokenizeAsync(
                value,
                cancellationToken);

            result.AddRange(tokens);
        }

        return result.OrderByDescending(x => x.MatchCount).ToList();
    }
}

public class DateContentTokenizer : IContentTokenizer
{
    private readonly Regex _dateExpression = new Regex(@"\d{1,2}\.\d{1,2}\.\d{4}");

    public string Name => "Date";

    public ValueTask<IReadOnlyList<ContentTokenData>> TokenizeAsync(
        string value,
        CancellationToken cancellationToken)
    {
        var result = new List<ContentTokenData>();

        var lines = value.Split('\n');

        foreach (var line in lines)
        {
            MatchCollection matches = _dateExpression.Matches(line);

            foreach (Match match in matches)
            {
                var date = DateTime.Parse(
                    match.Value,
                    CultureInfo.CreateSpecificCulture("de-CH"))
                    .ToString("s");

                ContentTokenData? existing = result
                    .FirstOrDefault(x => x.Value == date);

                if (existing is { })
                {
                    existing.Displays.Add(GetDisplay(match, line));
                    existing.MatchCount++;
                }
                else
                {
                    result.Add(new()
                    {
                        Tokenizer = Name,
                        Value = date,
                        Displays = new List<string>()
                        {
                            GetDisplay(match, line)
                        },
                        MatchCount = 1
                    });
                }
            }
        }

        return ValueTask.FromResult((IReadOnlyList<ContentTokenData>)result);
    }

    private string GetDisplay(Match match, string value)
    {
        var start = value.Substring(0, match.Index);
        var highlight = match.Value;
        var end = value.Substring(match.Index + match.Length);

        return $"{start}<i>{highlight}</i>{end}";
    }
}



