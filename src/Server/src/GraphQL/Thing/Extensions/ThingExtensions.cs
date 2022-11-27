namespace TagIt.GraphQL;

[ExtendObjectType(typeof(Thing))]
public class ThingExtensions
{
    public async Task<ThingType?> GetTypeAsync(
        [DataLoader] ThingTypeByIdDataLoader dataLoader,
        [Parent] Thing thing,
        CancellationToken cancellationToken)
    {
        if (thing.TypeId is { })
        {
            return await dataLoader.LoadAsync(thing.TypeId.Value, cancellationToken);
        }
        return null;
    }
}





