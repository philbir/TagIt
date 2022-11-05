using System.Security.Cryptography;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;

namespace TagIt;

public class OpenIdConnectDiscoveryService : IOpenIdConnectDiscoveryService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OpenIdConnectDiscoveryService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<DiscoveryDocumentResponse> GetAsync(string authority)
    {
        var cache = new DiscoveryCache(
            authority,
            _httpClientFactory.CreateClient,
            new DiscoveryPolicy
            {
                ValidateIssuerName = false,
                ValidateEndpoints = false,
            });

        DiscoveryDocumentResponse meta = await cache.GetAsync();
        return meta;
    }

    public IReadOnlyList<SecurityKey> GetSecurityKeys(DiscoveryDocumentResponse discovery)
    {
        var keys = new List<SecurityKey>();
        foreach (IdentityModel.Jwk.JsonWebKey? webKey in discovery.KeySet.Keys)
        {
            var e = Base64Url.Decode(webKey.E);
            var n = Base64Url.Decode(webKey.N);

            var key = new RsaSecurityKey(new RSAParameters { Exponent = e, Modulus = n })
            {
                KeyId = webKey.Kid
            };

            keys.Add(key);
        }

        return keys;
    }
}

