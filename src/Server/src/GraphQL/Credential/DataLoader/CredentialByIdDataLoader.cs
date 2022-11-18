using TagIt.Store;

namespace TagIt.GraphQL;

public class CredentialByIdDataLoader
    : BatchDataLoader<Guid, Credential?>
{
    private readonly ICredentialsStore _store;

    public CredentialByIdDataLoader(
        ICredentialsStore store,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _store = store;
    }

    protected override async Task<IReadOnlyDictionary<Guid, Credential?>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        IEnumerable<Credential> types =
            await _store.GetManyAsync(keys, cancellationToken);

        return types.ToDictionary(x => x.Id)!;
    }
}



