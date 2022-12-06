namespace TagIt.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class WebHookQueries
{
    public Task<IQueryable<WebHook>> GetWebHooks(
        [Service] IWebHookService service, CancellationToken cancellationToken)
            => service.Query(cancellationToken);
}
