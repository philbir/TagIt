namespace TagIt.GraphQL;

[ExtendObjectType(OperationTypeNames.Mutation)]
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

