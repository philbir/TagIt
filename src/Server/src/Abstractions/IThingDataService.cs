namespace TagIt;

public interface IThingDataService
{
    Task<ThingData> GetOriginalAsync(Thing thing, CancellationToken cancellationToken);
    Task AddDataAsync(Thing thing, AddThingDataRequest request, CancellationToken cancellationToken);

    Task<ThingData> GetByTypeAsync(
        Thing thing,
        string type,
        CancellationToken cancellationToken);

    Task<ThingData> GetPdfArchiveAsync(
        Thing thing,
        CancellationToken cancellationToken);
}

public class AddThingDataRequest
{
    public Stream Stream { get; set; }

    public string Filename { get; set; }

    public string Type { get; set; }
}
