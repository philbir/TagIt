namespace TagIt.GraphQL;

[MutationType]
public class ThingMutations
{
    public Task<Thing> UpdateThingAsync(
        [Service] IThingService service,
        UpdateThingInput input, CancellationToken cancellationToken)
        => service.UpdateThingAsync(input.ToRequest(), cancellationToken);
}
