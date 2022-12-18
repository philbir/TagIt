namespace TagIt.GraphQL;

[QueryType]
public class WebHookQueries
{
    public Task<IQueryable<WebHook>> GetWebHooks(
        [Service] IWebHookService service, CancellationToken cancellationToken)
            => service.Query(cancellationToken);
}
