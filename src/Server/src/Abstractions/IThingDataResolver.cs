namespace TagIt;

public interface IThingDataResolver
{
    Task<ThingData> GetOriginalAsync(Thing thing, CancellationToken cancellationToken);
}