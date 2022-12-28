namespace TagIt;

public interface IThingDataService
{
    Task<ThingData> GetOriginalAsync(Thing thing, CancellationToken cancellationToken);
}