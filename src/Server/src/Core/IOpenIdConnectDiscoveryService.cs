using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;

namespace TagIt;

public interface IOpenIdConnectDiscoveryService
{
    Task<DiscoveryDocumentResponse> GetAsync(string authority);
    IReadOnlyList<SecurityKey> GetSecurityKeys(DiscoveryDocumentResponse discovery);
}