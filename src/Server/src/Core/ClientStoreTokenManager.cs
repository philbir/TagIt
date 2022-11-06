using IdentityModel.Client;
using Serilog;

namespace TagIt;

public class CredentialStoreTokenManager : ICredentialStoreTokenManager
{
    private readonly ICredentialStoreService _credentialStoreService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOpenIdConnectDiscoveryService _openIdConnectDiscoveryService;

    public CredentialStoreTokenManager(
        ICredentialStoreService credentialStoreService,
        IHttpClientFactory httpClientFactory,
        IOpenIdConnectDiscoveryService openIdConnectDiscoveryService)
    {
        _credentialStoreService = credentialStoreService;
        _httpClientFactory = httpClientFactory;
        _openIdConnectDiscoveryService = openIdConnectDiscoveryService;
    }

    public async Task<string> GetAccessToken(Guid id, CancellationToken cancellationToken)
    {
        Credential creds = await _credentialStoreService.GetByIdAsync(id, cancellationToken);

        CredentialToken? accessToken = creds.Tokens
            .FirstOrDefault(x => x.Type == TokenType.Access);

        if (accessToken == null)
        {
            throw new ApplicationException("No access token found");
        }

        if (accessToken.ExpiresAt > DateTime.UtcNow)
        {
            return accessToken.Value.Value;
        }
        else
        {
            Log.Information("Access token expired, refreshing");

            CredentialToken? refreshToken = creds.Tokens
                .FirstOrDefault(x => x.Type == TokenType.Refresh);

            if (refreshToken == null)
            {
                throw new ApplicationException("Access token expired an no refresh token found");
            }

            //Refresh
            DiscoveryDocumentResponse meta = await _openIdConnectDiscoveryService.GetAsync(creds.Client.Authority);

            HttpClient httpClient = _httpClientFactory.CreateClient();

            TokenResponse tokenResonse = await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = meta.TokenEndpoint,
                ClientId = creds.Client.Id,
                ClientSecret = creds.Client.Secret.Value,
                RefreshToken = refreshToken.Value.Value
            });

            if (tokenResonse.IsError)
            {
                throw new ApplicationException("Could not refresh token");
            }

            accessToken.Value.Value = tokenResonse.AccessToken;
            accessToken.ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResonse.ExpiresIn);

            refreshToken.Value.Value = tokenResonse.RefreshToken;

            await _credentialStoreService.UpdateAsync(creds, cancellationToken);

            return tokenResonse.AccessToken;
        }
    }
}

