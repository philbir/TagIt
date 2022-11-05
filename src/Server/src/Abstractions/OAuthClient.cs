#nullable disable


namespace TagIt;

public class OAuthClient
{
    public string Id { get; set; }

    public string? Product { get; set; }

    public ProtectedValue Secret { get; set; }

    public string Authority { get; set; }

    public IReadOnlyList<string> Scopes { get; set; }

    public OAuthFlow Flow { get; set; }
}
