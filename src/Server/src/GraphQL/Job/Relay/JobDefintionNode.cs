using TagIt.Jobs;

namespace TagIt.GraphQL;

[Node]
[ExtendObjectType(typeof(JobDefintion))]
public sealed class JobDefintionNode
{
    [NodeResolver]
    public static Task<JobDefintion?> GetThingAsync(
        [Service] IJobDefintionService service,
        Guid id,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(id, cancellationToken);
    }
}
