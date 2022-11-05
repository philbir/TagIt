#nullable disable


using TagIt.Store;

namespace TagIt;

public class CredentialToken
{
    public Guid Id { get; set; }

    public TokenType Type { get; set; }

    public ProtectedValue Value { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? ExpiresAt { get; set; }

    public static CredentialToken Create(
        TokenType type,
        string value,
        DateTime? expiresAt)
    {
        return new CredentialToken
        {
            Id = Guid.NewGuid(),
            Type = type,
            Value = new ProtectedValue {  Value = value},
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = expiresAt
        };
    }
}

public class ClientAuthState : IEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string RedirectUri { get; set; }

    public Guid CredentialId { get; set; }

    public string Nonce { get; set; }
}
