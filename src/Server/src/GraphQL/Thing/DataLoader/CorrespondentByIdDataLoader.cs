namespace TagIt.GraphQL;

public class CorrespondentByIdDataLoader
    : BatchDataLoader<Guid, Correspondent?>
{
    private readonly ICorrespondentService _service;

    public CorrespondentByIdDataLoader(
        ICorrespondentService service,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _service = service;
    }

    protected override async Task<IReadOnlyDictionary<Guid, Correspondent?>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        IEnumerable<Correspondent> classes =
            await _service.GetManyAsync(keys, cancellationToken);

        return classes.ToDictionary(x => x.Id)!;
    }
}
