using System.Text.RegularExpressions;

namespace TagIt;

public class ContentDetectorService : IContentDetectorService
{
    public IReadOnlyList<DetectResult<T>> Detect<T>(
        IEnumerable<T> items,
        IThingContentAccessor contentAccessor) where T : IDetectable
            => Detect(items, contentAccessor.GetAllText());

    public IReadOnlyList<DetectResult<T>> Detect<T>(IEnumerable<T> items, string value) where T : IDetectable
    {
        var results = new List<DetectResult<T>>();

        foreach (var item in items)
        {
            var rules = item.DetectRules?.ToList();
            if (rules is {} && !rules.Any())
            {
                rules.Add(new() { Expression = item.Name.ToLower(), Mode = DetectRuleMode.Regex, Weight = 1 });
            }

            int score = 0;
            foreach (DetectRule rule in rules)
            {
                if (rule.Mode == DetectRuleMode.Regex)
                {
                    MatchCollection matches =
                        new Regex(rule.Expression, RegexOptions.Compiled | RegexOptions.IgnoreCase)
                            .Matches(value);

                    score += matches.Count * rule.Weight;
                }
            }

            if (score > 0)
            {
                results.Add(new DetectResult<T>(item, score));
            }
        }

        return results.OrderByDescending(x => x.Scrore).ToList();
    }
}
