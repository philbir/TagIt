namespace TagIt.Store;

public interface IWebHookStore
{
    public IQueryable<WebHook> Query();

    Task<IReadOnlyList<WebHook>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken);

    Task<WebHook> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<WebHook?> GetByJobIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<WebHook> InsertAsync(
        WebHook webHook,
        CancellationToken cancellationToken);

    Task<WebHook> UpdateAsync(WebHook webHook, CancellationToken cancellationToken);

    Task<long> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
