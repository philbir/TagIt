using TagIt.Store;
namespace TagIt;

public class CredentialStoreService : ICredentialStoreService
{
    private readonly ICredentialsStore _credentialsStore;
    private readonly IValueProtector _valueProtector;

    public CredentialStoreService(
        ICredentialsStore credentialsStore,
        IValueProtector valueProtector)
    {
        _credentialsStore = credentialsStore;
        _valueProtector = valueProtector;
    }

    public Task<IQueryable<Credential>> Query(
        CancellationToken cancellationToken)
    {
        return Task.FromResult(_credentialsStore.Query());
    }

    public async Task<Credential> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Credential creds = await _credentialsStore
            .GetByIdAsync(id, cancellationToken);

        await UnProtectAsync(creds, cancellationToken);

        return creds;
    }

    public Task<Credential> RegisterOAuthClientAsync(
        string name,
        OAuthClient client,
        CancellationToken cancellationToken)
             => AddAsync(new Credential
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Client = client,
                    }, cancellationToken);


    public async Task<Credential> AddAsync(
        Credential credential,
        CancellationToken cancellationToken)
    {
        await ProtectAsync(credential, cancellationToken);

        Credential result = await _credentialsStore.InsertAsync(
            credential,
            cancellationToken);

        return result;
    }

    public async Task<Credential> UpdateAsync(
        Credential credential,
        CancellationToken cancellationToken)
    {
        await ProtectAsync(credential, cancellationToken);

        return  await _credentialsStore.UpdateAsync(credential, cancellationToken);
    }

    private async Task UnProtectAsync(
        Credential credential,
        CancellationToken cancellationToken)
    {
        if (credential.Client?.Secret is { })
        {
            credential.Client.Secret = await UnProtectValueAsync(
                credential.Client.Secret,
                cancellationToken);
        }

        foreach (CredentialToken? token in credential.Tokens)
        {
            token.Value = await UnProtectValueAsync(token.Value, cancellationToken);
        }
    }

    private async Task ProtectAsync(
        Credential credential,
        CancellationToken cancellationToken)
    {
        if (credential.Client?.Secret is { })
        {
            credential.Client.Secret = await ProtectValueAsync(
                credential.Client.Secret,
                cancellationToken);
        }

        foreach (CredentialToken? token in credential.Tokens)
        {
            token.Value = await ProtectValueAsync(token.Value, cancellationToken);
        }
    }

    private async Task<ProtectedValue> UnProtectValueAsync(
        ProtectedValue value,
        CancellationToken cancellationToken)
    {
        string clearText = await _valueProtector.UnProtectAsync(
            value.Cipher,
            cancellationToken);

        return value with { Value = clearText };
    }

    private async Task<ProtectedValue> ProtectValueAsync(
        ProtectedValue value,
        CancellationToken cancellationToken)
    {
        var cipher = await _valueProtector.ProtectAsync(
            value.Value,
            cancellationToken);

        return value with { Cipher = cipher };
    }
}
