namespace TagIt;

public interface IWebHookService
{
    Task<WebHook> CreateAsync(
        CreateHookRequest createRequest,
        CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<WebHook> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<WebHook?> GetByJobIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<WebHook>> Query(CancellationToken cancellationToken);
    Task UpdateAsync(WebHook webHook, CancellationToken cancellationToken);
}

public record CreateHookRequest(string Product, Guid ConnectorId, Guid JobId);
