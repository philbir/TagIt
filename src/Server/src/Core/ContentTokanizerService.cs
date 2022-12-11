using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TagIt;

public class TokenExtractorService
{
    public async ValueTask<IEnumerable<TokenData>> TokenizeAsync(
        string value,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class DateContentTokenizer : IContentTokenizer
{
    Regex dateExpression = new Regex(@"\d{1,2}\.\d{1,2}\.\d{4}");

    public string Name { get; } = "Date";

    public ValueTask<IReadOnlyList<TokenData>> TokenizeAsync(string value, CancellationToken cancellationToken)
    {
        MatchCollection matches = dateExpression.Matches(value);

        var result = new List<TokenData>();

        foreach (Match match in matches)
        {
            var date = DateTime.Parse(match.Value, CultureInfo.CreateSpecificCulture("de-CH"));

            var tokenData = new TokenData
            {
                Value = date, Index = match.Index, Length = match.Length, Display = GetDisplay(match, value)
            };

            result.Add(tokenData);
        }

        return ValueTask.FromResult((IReadOnlyList<TokenData>)result);
    }

    private string GetDisplay(Match match, string value)
    {
        var start = value.Substring(0, match.Index);
        var highlight = match.Value;
        var end = value.Substring(match.Index + match.Length);

        return $"{start}</i>{highlight}</i>{end}";
    }
}

public interface IContentTokenizer
{
    string Name { get; }
    ValueTask<IReadOnlyList<TokenData>> TokenizeAsync(string value, CancellationToken cancellationToken);
}

public class TokenData
{
    public string Type { get; set; }
    public object Value { get; set; }
    public int Index { get; set; }
    public int Length { get; set; }
    public string Display { get; set; }

    public override string ToString()
    {
        return Display;
    }
}
