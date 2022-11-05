namespace TagIt;

public interface ICredentialStoreTokenManager
{
    Task<string> GetAccessToken(Guid id, CancellationToken cancellationToken);
}