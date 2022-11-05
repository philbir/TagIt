using System.Security.Cryptography;
using IdentityModel.Client;
using TagIt.Store;

namespace TagIt.ClientAppAuthorization;

public class ClientAuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ICredentialStoreService _credentialStoreService;
    private readonly IOpenIdConnectDiscoveryService _openIdConnectDiscoveryService;
    private readonly IClientAuthStateStore _clientAuthStateStore;

    public ClientAuthorizationMiddleware(
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

        RouteValueDictionary routeData = context.GetRouteData().Values;

        var id = Guid.Parse(routeData["id"].ToString());

        Credential creds = await _credentialStoreService.GetByIdAsync(
            id,
            context.RequestAborted);

        DiscoveryDocumentResponse meta = await _openIdConnectDiscoveryService
            .GetAsync(creds.Client.Authority);

        var authState = new ClientAuthState
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            CredentialId = creds.Id,
            Nonce = RandomNumberGenerator.GetInt32(int.MaxValue).ToString(),
            RedirectUri = BuildRedirectUri(context)
        };
        await _clientAuthStateStore.InsertAsync(authState, context.RequestAborted);

        var authorize = new RequestUrl(meta.AuthorizeEndpoint)
            .CreateAuthorizeUrl(
                clientId: creds.Client.Id,
                responseType: "code",
                scope: string.Join(' ', creds.Client.Scopes),
                redirectUri: authState.RedirectUri,
                state: authState.Id.ToString("N"),
                nonce: authState.Nonce,
                responseMode: "form_post");

        context.Response.Redirect(authorize);

        //await _next(context);

        return;
    }

    private string BuildRedirectUri(HttpContext context)
    {
        return context.Request.Scheme + "://" + context.Request.Host.ToString() + "/clients/callback";
    }
}
