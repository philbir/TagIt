using TagIt.Jobs;

namespace TagIt.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class JobQueries
{
    [UsePaging]
    [UseFiltering]
    public Task<IQueryable<JobDefintion>> GetJobDefinitionsAsync(
        [Service] IJobDefintionService service,
        CancellationToken cancellationToken)
    {
        return service.Query(cancellationToken);
    }
}
