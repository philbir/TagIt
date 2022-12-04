namespace TagIt.GraphQL;

public class ReceiverByIdDataLoader
    : BatchDataLoader<Guid, Receiver?>
{
    private readonly IReceiverService _service;

    public ReceiverByIdDataLoader(
        IReceiverService service,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _service = service;
    }

    protected override async Task<IReadOnlyDictionary<Guid, Receiver?>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        IEnumerable<Receiver> classes =
            await _service.GetManyAsync(keys, cancellationToken);

        return classes.ToDictionary(x => x.Id)!;
    }
}
