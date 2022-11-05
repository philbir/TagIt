#nullable disable

using TagIt.Store;

namespace TagIt;

public class Credential : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public OAuthClient Client { get; set; }

    public IReadOnlyList<CredentialToken> Tokens { get; set; }
        = new List<CredentialToken>();
}
