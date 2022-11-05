using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using TagIt.Configuration;
using TagIt.Store;

namespace TagIt;

public class WebHookService : IWebHookService
{
    private readonly IWebHookStore _store;
    private readonly IOptionsMonitor<TagItServerOptions> _options;

    public WebHookService(
        IWebHookStore store,
        IOptionsMonitor<TagItServerOptions> options)
    {
        _store = store;
        _options = options;
    }

    public async Task<WebHook> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _store.GetByIdAsync(id, cancellationToken);
    }

    public Task<WebHook?> GetByJobIdAsync(Guid id, CancellationToken cancellationToken)
        => _store.GetByJobIdAsync(id, cancellationToken);

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _store.DeleteAsync(id, cancellationToken);

        return deleted > 0;
    }

    public Task<IQueryable<WebHook>> Query(CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.Query());
    }

    public async Task<WebHook> CreateAsync(
        CreateHookRequest createRequest,
        CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var url = $"{_options.Get(nameof(TagItServerOptions)).PublicUrl.TrimEnd(new char[] { '/' })}" +
            $"/hooks/{id:N}";

        var hook = new WebHook
        {
            Id = id,
            Product = createRequest.Product,
            CreatedAt = DateTime.UtcNow,
            ConnectorId = createRequest.ConnectorId,
            JobId = createRequest.JobId,
            Url = url,
            ClientState = RandomNumberGenerator.GetInt32(int.MaxValue / 2, int.MaxValue).ToString()
        };

        return await _store.InsertAsync(hook, cancellationToken);
    }

    public Task UpdateAsync(WebHook webHook, CancellationToken cancellationToken)
        => _store.UpdateAsync(webHook, cancellationToken);
}
