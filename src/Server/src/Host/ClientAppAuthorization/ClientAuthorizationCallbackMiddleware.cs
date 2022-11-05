using System.IdentityModel.Tokens.Jwt;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
using TagIt.Store;
using System.Security.Claims;

namespace TagIt.ClientAppAuthorization;

public class ClientAuthorizationCallbackMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ICredentialStoreService _credentialStoreService;
    private readonly IOpenIdConnectDiscoveryService _openIdConnectDiscoveryService;
    private readonly IClientAuthStateStore _clientAuthStateStore;

    public ClientAuthorizationCallbackMiddleware(
        RequestDelegate next,
        ICredentialStoreService credentialStoreService,
        IOpenIdConnectDiscoveryService openIdConnectDiscoveryService,
        IClientAuthStateStore clientAuthStateStore)
    {
        _next = next;
        _credentialStoreService = credentialStoreService;
        _openIdConnectDiscoveryService = openIdConnectDiscoveryService;
        _clientAuthStateStore = clientAuthStateStore;
    }

    public async Task Invoke(HttpContext context)
    {
        Endpoint? endpoint = context.GetEndpoint();
        if (endpoint == null)
        {
            await _next(context);
            return;
        }

        var stateId = context.Request.Form["state"].ToString();
        var code = context.Request.Form["code"].ToString();

        ClientAuthState authState = await _clientAuthStateStore.GetByIdAsync(
            Guid.Parse(stateId),
            context.RequestAborted);

        Credential creds = await _credentialStoreService.GetByIdAsync(
            authState.CredentialId,
            context.RequestAborted);

        DiscoveryDocumentResponse meta = await _openIdConnectDiscoveryService
            .GetAsync(creds.Client.Authority);

        var client = new HttpClient();
        TokenResponse tokenResponse = await client.RequestAuthorizationCodeTokenAsync(
            new AuthorizationCodeTokenRequest
            {
                Address = meta.TokenEndpoint,
                ClientId = creds.Client.Id,
                ClientSecret = creds.Client.Secret.Value,
                Code = code,
                RedirectUri = authState.RedirectUri
            }, context.RequestAborted);

        await _clientAuthStateStore.DeleteAsync(authState.Id, context.RequestAborted);

        JwtSecurityToken jwt = ValidateToken(tokenResponse.IdentityToken, meta);

        if (!ValidateNonce(jwt, authState))
        {
            throw new SecurityTokenException("Invalid nonce");
        }

        var tokens = new List<CredentialToken>
        {
            CredentialToken.Create(
                TokenType.Access,
                tokenResponse.AccessToken,
                DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn)),

            CredentialToken.Create(
                TokenType.Id,
                tokenResponse.IdentityToken,
                null)
        };

        if (tokenResponse.RefreshToken != null)
        {
            tokens.Add(CredentialToken.Create(
                TokenType.Refresh,
                tokenResponse.RefreshToken,
                null));
        }

        creds.Tokens = tokens;

        await _credentialStoreService.UpdateAsync(creds, context.RequestAborted);

        return;
    }

    private bool ValidateNonce(JwtSecurityToken jwt, ClientAuthState authState)
    {
        Claim? nonceClaim = jwt.Claims.FirstOrDefault(x => x.Type == "nonce");

        if (nonceClaim is { })
        {
            return nonceClaim.Value == authState.Nonce;
        }

        return false;

    }

    private JwtSecurityToken ValidateToken(
        string token,
        DiscoveryDocumentResponse discoveryDocument)
    {
        var validator = new JwtSecurityTokenHandler();
        if (!validator.CanReadToken(token))
        {
            throw new SecurityTokenException("Can not read token");
        }

        var tokenValidation = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKeys = _openIdConnectDiscoveryService.GetSecurityKeys(discoveryDocument)
        };

        validator.ValidateToken(token, tokenValidation, out SecurityToken validatedToken);

        if (validatedToken is JwtSecurityToken validatedJwt)
        {
            return validatedJwt;
        }
        else
        {
            throw new SecurityTokenException("Token validation failed");
        }
    }
}

public static class AzureADIssuerValidation
{
    internal static string Validate(
        string issuer,
        SecurityToken token,
        TokenValidationParameters parameters)
    {
        if (token is JwtSecurityToken jwt)
        {
            if (jwt.Payload.TryGetValue("tid", out var value) &&
                value is string tokenTenantId)
            {
                IEnumerable<string> validIssuers = (parameters.ValidIssuers ?? Enumerable.Empty<string>())
                    .Append(parameters.ValidIssuer)
                    .Where(i => !string.IsNullOrEmpty(i));

                if (validIssuers.Any(i => i.Replace("{tenantid}", tokenTenantId) == issuer))
                    return issuer;
            }
        }

        throw new SecurityTokenInvalidIssuerException("Bla")
        {
            InvalidIssuer = "Foo"
        };
    }

}

