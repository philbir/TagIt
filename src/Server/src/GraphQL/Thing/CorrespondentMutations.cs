namespace TagIt.GraphQL;

[MutationType]
public class CorrespondentMutations
{
    public async Task<Correspondent> InsertCorrespondentAsync(
        [Service] ICorrespondentService service,
        string name,
        CancellationToken cancellationToken)
    {
        return await service.AddAsync(
            name,
            cancellationToken);
    }
}

